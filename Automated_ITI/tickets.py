"""
Ticket Management System
Handles ticket creation, updates, and management with SLA tracking
"""

import json
import uuid
from datetime import datetime, timedelta
from typing import List, Dict, Optional
from utils import Logger, FileManager

class Ticket:
    """Base class for all tickets"""
    
    def __init__(self, employee_name: str, department: str, category: str, 
                 issue_description: str):
        self.ticket_id = self._generate_ticket_id()
        self.employee_name = employee_name
        self.department = department
        self.category = category
        self.issue_description = issue_description
        self.priority = self._assign_priority(category)
        self.status = "Open"
        self.created_datetime = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        self.updated_datetime = self.created_datetime
        self.sla_deadline = self._calculate_sla_deadline()
        self.sla_breached = False
        
    def _generate_ticket_id(self) -> str:
        """Generate unique ticket ID"""
        return f"TICKET-{str(uuid.uuid4())[:8].upper()}"
        
    def _assign_priority(self, category: str) -> str:
        """Assign priority based on issue category"""
        priority_mapping = {
            "Server Down": "P1",
            "Internet Down": "P2", 
            "Laptop Slow": "P3",
            "Password Reset": "P4",
            "Printer Not Working": "P3",
            "Outlook Not Opening": "P3",
            "Application Crash": "P2",
            "Other": "P4"
        }
        return priority_mapping.get(category, "P4")
        
    def _calculate_sla_deadline(self) -> str:
        """Calculate SLA deadline based on priority"""
        sla_hours = {
            "P1": 1,
            "P2": 4,
            "P3": 8,
            "P4": 24
        }
        
        hours = sla_hours.get(self.priority, 24)
        deadline = datetime.now() + timedelta(hours=hours)
        return deadline.strftime("%Y-%m-%d %H:%M:%S")
        
    def update_status(self, new_status: str):
        """Update ticket status and timestamp"""
        self.status = new_status
        self.updated_datetime = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        
    def check_sla_breach(self) -> bool:
        """Check if SLA has been breached"""
        if self.status == "Closed":
            return False
            
        current_time = datetime.now()
        deadline_time = datetime.strptime(self.sla_deadline, "%Y-%m-%d %H:%M:%S")
        
        if current_time > deadline_time:
            self.sla_breached = True
            return True
        return False
        
    def to_dict(self) -> Dict:
        """Convert ticket to dictionary for JSON serialization"""
        return {
            "ticket_id": self.ticket_id,
            "employee_name": self.employee_name,
            "department": self.department,
            "category": self.category,
            "issue_description": self.issue_description,
            "priority": self.priority,
            "status": self.status,
            "created_datetime": self.created_datetime,
            "updated_datetime": self.updated_datetime,
            "sla_deadline": self.sla_deadline,
            "sla_breached": self.sla_breached
        }
        
    @classmethod
    def from_dict(cls, data: Dict):
        """Create ticket from dictionary"""
        ticket = cls(
            data["employee_name"],
            data["department"], 
            data["category"],
            data["issue_description"]
        )
        ticket.ticket_id = data["ticket_id"]
        ticket.priority = data["priority"]
        ticket.status = data["status"]
        ticket.created_datetime = data["created_datetime"]
        ticket.updated_datetime = data["updated_datetime"]
        ticket.sla_deadline = data["sla_deadline"]
        ticket.sla_breached = data.get("sla_breached", False)
        return ticket


class IncidentTicket(Ticket):
    """Incident Management Ticket - inherits from Ticket"""
    
    def __init__(self, employee_name: str, department: str, category: str, 
                 issue_description: str, impact: str = "Medium"):
        super().__init__(employee_name, department, category, issue_description)
        self.ticket_type = "Incident"
        self.impact = impact
        self.urgency = self._calculate_urgency()
        
    def _calculate_urgency(self) -> str:
        """Calculate urgency based on impact and priority"""
        if self.priority == "P1" or self.impact == "High":
            return "High"
        elif self.priority == "P2" or self.impact == "Medium":
            return "Medium"
        else:
            return "Low"


class ServiceRequest(Ticket):
    """Service Request Ticket - inherits from Ticket"""
    
    def __init__(self, employee_name: str, department: str, category: str, 
                 issue_description: str, request_type: str = "Standard"):
        super().__init__(employee_name, department, category, issue_description)
        self.ticket_type = "Service Request"
        self.request_type = request_type
        self.approval_status = "Pending" if request_type == "Special" else "Approved"


class ProblemRecord:
    """Problem Management Record for recurring issues"""
    
    def __init__(self, problem_description: str, related_tickets: List[str]):
        self.problem_id = f"PROB-{str(uuid.uuid4())[:8].upper()}"
        self.problem_description = problem_description
        self.related_tickets = related_tickets
        self.status = "Open"
        self.root_cause = ""
        self.solution = ""
        self.created_datetime = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        self.updated_datetime = self.created_datetime
        
    def update_solution(self, root_cause: str, solution: str):
        """Update problem with root cause and solution"""
        self.root_cause = root_cause
        self.solution = solution
        self.status = "Resolved"
        self.updated_datetime = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        
    def to_dict(self) -> Dict:
        """Convert problem record to dictionary"""
        return {
            "problem_id": self.problem_id,
            "problem_description": self.problem_description,
            "related_tickets": self.related_tickets,
            "status": self.status,
            "root_cause": self.root_cause,
            "solution": self.solution,
            "created_datetime": self.created_datetime,
            "updated_datetime": self.updated_datetime
        }


class TicketManager:
    """Manages all ticket operations"""
    
    def __init__(self):
        self.tickets: List[Ticket] = []
        self.problem_records: List[ProblemRecord] = []
        self.logger = Logger()
        self.file_manager = FileManager()
        self.load_tickets()
        
    def create_ticket(self, employee_name: str, department: str, category: str, 
                     issue_description: str) -> Ticket:
        """Create a new ticket"""
        if category in ["Server Down", "Internet Down", "Application Crash"]:
            ticket = IncidentTicket(employee_name, department, category, issue_description)
        else:
            ticket = ServiceRequest(employee_name, department, category, issue_description)
            
        self.tickets.append(ticket)
        self.save_tickets()
        
        # Check for recurring issues (ITIL Problem Management)
        self.check_recurring_issues(category, issue_description)
        
        return ticket
        
    def get_all_tickets(self) -> List[Ticket]:
        """Get all tickets"""
        return self.tickets
        
    def get_ticket_by_id(self, ticket_id: str) -> Optional[Ticket]:
        """Get ticket by ID"""
        for ticket in self.tickets:
            if ticket.ticket_id == ticket_id:
                return ticket
        return None
        
    def update_ticket_status(self, ticket_id: str, new_status: str) -> bool:
        """Update ticket status"""
        ticket = self.get_ticket_by_id(ticket_id)
        if ticket:
            ticket.update_status(new_status)
            self.save_tickets()
            return True
        return False
        
    def close_ticket(self, ticket_id: str) -> bool:
        """Close a ticket"""
        return self.update_ticket_status(ticket_id, "Closed")
        
    def delete_ticket(self, ticket_id: str) -> bool:
        """Delete a ticket"""
        ticket = self.get_ticket_by_id(ticket_id)
        if ticket:
            self.tickets.remove(ticket)
            self.save_tickets()
            return True
        return False
        
    def check_sla_breaches(self) -> List[Ticket]:
        """Check all tickets for SLA breaches"""
        breached_tickets = []
        for ticket in self.tickets:
            if ticket.check_sla_breach():
                breached_tickets.append(ticket)
                self.logger.log_event(f"SLA Breached: {ticket.ticket_id}")
                
        return breached_tickets
        
    def get_escalation_alerts(self) -> List[Dict]:
        """Get tickets requiring escalation"""
        alerts = []
        current_time = datetime.now()
        
        for ticket in self.tickets:
            if ticket.status == "Closed":
                continue
                
            created_time = datetime.strptime(ticket.created_datetime, "%Y-%m-%d %H:%M:%S")
            time_elapsed = current_time - created_time
            
            # P1 escalation after 30 minutes
            if ticket.priority == "P1" and time_elapsed > timedelta(minutes=30):
                alerts.append({
                    "ticket_id": ticket.ticket_id,
                    "priority": ticket.priority,
                    "reason": "P1 ticket unresolved after 30 minutes",
                    "time_elapsed": str(time_elapsed)
                })
                
            # P2 escalation after 2 hours
            elif ticket.priority == "P2" and time_elapsed > timedelta(hours=2):
                alerts.append({
                    "ticket_id": ticket.ticket_id,
                    "priority": ticket.priority,
                    "reason": "P2 ticket unresolved after 2 hours",
                    "time_elapsed": str(time_elapsed)
                })
                
            # Any breached SLA
            elif ticket.sla_breached:
                alerts.append({
                    "ticket_id": ticket.ticket_id,
                    "priority": ticket.priority,
                    "reason": "SLA Breached",
                    "time_elapsed": str(time_elapsed)
                })
                
        return alerts
        
    def check_recurring_issues(self, category: str, issue_description: str):
        """Check for recurring issues and create problem records"""
        # Count similar issues
        similar_tickets = []
        for ticket in self.tickets:
            if ticket.category == category:
                # Simple similarity check - in real system would use more sophisticated matching
                if category.lower() in issue_description.lower():
                    similar_tickets.append(ticket.ticket_id)
                    
        # If same issue occurs 5 times, create problem record
        if len(similar_tickets) >= 5:
            problem_desc = f"Recurring issue: {category} - {issue_description[:50]}..."
            
            # Check if problem record already exists
            existing_problem = None
            for problem in self.problem_records:
                if category in problem.problem_description:
                    existing_problem = problem
                    break
                    
            if existing_problem:
                existing_problem.related_tickets.extend(similar_tickets[-5:])
            else:
                problem = ProblemRecord(problem_desc, similar_tickets[-5:])
                self.problem_records.append(problem)
                self.logger.log_event(f"Problem Record created: {problem.problem_id}")
                
    def save_tickets(self):
        """Save tickets to JSON file"""
        try:
            tickets_data = {
                "tickets": [ticket.to_dict() for ticket in self.tickets],
                "problem_records": [problem.to_dict() for problem in self.problem_records]
            }
            self.file_manager.save_tickets(tickets_data)
            
            # Create backup
            self.file_manager.create_backup(self.tickets)
            
        except Exception as e:
            self.logger.log_event(f"Error saving tickets: {e}")
            
    def load_tickets(self):
        """Load tickets from JSON file"""
        try:
            data = self.file_manager.load_tickets()
            
            if data and "tickets" in data:
                self.tickets = [Ticket.from_dict(ticket_data) for ticket_data in data["tickets"]]
                
            if data and "problem_records" in data:
                for problem_data in data["problem_records"]:
                    problem = ProblemRecord(
                        problem_data["problem_description"],
                        problem_data["related_tickets"]
                    )
                    problem.problem_id = problem_data["problem_id"]
                    problem.status = problem_data["status"]
                    problem.root_cause = problem_data["root_cause"]
                    problem.solution = problem_data["solution"]
                    problem.created_datetime = problem_data["created_datetime"]
                    problem.updated_datetime = problem_data["updated_datetime"]
                    self.problem_records.append(problem)
                    
        except Exception as e:
            self.logger.log_event(f"Error loading tickets: {e}")
            
    def get_statistics(self) -> Dict:
        """Get ticket statistics"""
        total = len(self.tickets)
        open_tickets = len([t for t in self.tickets if t.status == "Open"])
        in_progress = len([t for t in self.tickets if t.status == "In Progress"])
        closed = len([t for t in self.tickets if t.status == "Closed"])
        high_priority = len([t for t in self.tickets if t.priority in ["P1", "P2"]])
        sla_breached = len([t for t in self.tickets if t.sla_breached])
        
        return {
            "total_tickets": total,
            "open_tickets": open_tickets,
            "in_progress": in_progress,
            "closed_tickets": closed,
            "high_priority": high_priority,
            "sla_breached": sla_breached
        }
