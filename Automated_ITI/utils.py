"""
Utility Functions
File handling, logging, and common utilities
"""

import json
import csv
import os
from datetime import datetime
from typing import Dict, List, Any

class FileManager:
    """Handles file operations for tickets and backups"""
    
    def __init__(self):
        self.data_dir = "data"
        self.tickets_file = os.path.join(self.data_dir, "tickets.json")
        self.logs_file = os.path.join(self.data_dir, "logs.txt")
        self.backup_file = os.path.join(self.data_dir, "backup.csv")
        
        # Ensure data directory exists
        os.makedirs(self.data_dir, exist_ok=True)
        
    def save_tickets(self, tickets_data: Dict):
        """Save tickets to JSON file"""
        try:
            with open(self.tickets_file, 'w', encoding='utf-8') as f:
                json.dump(tickets_data, f, indent=2, ensure_ascii=False)
        except Exception as e:
            raise Exception(f"Failed to save tickets: {e}")
            
    def load_tickets(self) -> Dict:
        """Load tickets from JSON file"""
        try:
            if os.path.exists(self.tickets_file):
                with open(self.tickets_file, 'r', encoding='utf-8') as f:
                    return json.load(f)
            return {}
        except json.JSONDecodeError as e:
            raise Exception(f"Failed to parse tickets file: {e}")
        except Exception as e:
            raise Exception(f"Failed to load tickets: {e}")
            
    def create_backup(self, tickets: List[Any]):
        """Create CSV backup of tickets"""
        try:
            with open(self.backup_file, 'w', newline='', encoding='utf-8') as csvfile:
                fieldnames = ['ticket_id', 'employee_name', 'department', 'category',
                             'issue_description', 'priority', 'status', 'created_datetime',
                             'updated_datetime', 'sla_deadline', 'sla_breached']
                writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
                
                writer.writeheader()
                for ticket in tickets:
                    writer.writerow(ticket.to_dict())
                    
        except Exception as e:
            raise Exception(f"Failed to create backup: {e}")
            
    def load_backup(self) -> List[Dict]:
        """Load tickets from CSV backup"""
        try:
            if not os.path.exists(self.backup_file):
                return []
                
            tickets = []
            with open(self.backup_file, 'r', encoding='utf-8') as csvfile:
                reader = csv.DictReader(csvfile)
                for row in reader:
                    tickets.append(dict(row))
            return tickets
            
        except Exception as e:
            raise Exception(f"Failed to load backup: {e}")


class Logger:
    """Handles system logging"""
    
    def __init__(self):
        self.data_dir = "data"
        self.logs_file = os.path.join(self.data_dir, "logs.txt")
        
        # Ensure data directory exists
        os.makedirs(self.data_dir, exist_ok=True)
        
    def log_event(self, message: str):
        """Log an event with timestamp"""
        try:
            timestamp = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
            log_entry = f"[{timestamp}] {message}\n"
            
            with open(self.logs_file, 'a', encoding='utf-8') as f:
                f.write(log_entry)
                
        except Exception as e:
            print(f"Failed to log event: {e}")
            
    def display_logs(self, lines: int = 50):
        """Display recent log entries"""
        try:
            if not os.path.exists(self.logs_file):
                print("No log file found.")
                return
                
            with open(self.logs_file, 'r', encoding='utf-8') as f:
                log_lines = f.readlines()
                
            # Show last 'lines' entries
            recent_logs = log_lines[-lines:] if len(log_lines) > lines else log_lines
            
            print(f"\n--- Last {len(recent_logs)} Log Entries ---")
            for line in recent_logs:
                print(line.strip())
                
        except Exception as e:
            print(f"Error reading logs: {e}")
            
    def clear_logs(self):
        """Clear all logs"""
        try:
            with open(self.logs_file, 'w', encoding='utf-8') as f:
                f.write("")
            self.log_event("Logs cleared")
            print("Logs cleared successfully.")
        except Exception as e:
            print(f"Error clearing logs: {e}")
            
    def get_log_stats(self) -> Dict:
        """Get logging statistics"""
        try:
            if not os.path.exists(self.logs_file):
                return {"total_entries": 0, "file_size": 0}
                
            with open(self.logs_file, 'r', encoding='utf-8') as f:
                lines = f.readlines()
                
            file_size = os.path.getsize(self.logs_file)
            
            return {
                "total_entries": len(lines),
                "file_size": file_size,
                "last_entry": lines[-1].strip() if lines else ""
            }
            
        except Exception as e:
            return {"error": str(e)}


class InputValidator:
    """Validates user input"""
    
    @staticmethod
    def validate_employee_name(name: str) -> bool:
        """Validate employee name"""
        if not name or not name.strip():
            return False
        if len(name.strip()) < 2:
            return False
        return True
        
    @staticmethod
    def validate_department(department: str) -> bool:
        """Validate department name"""
        if not department or not department.strip():
            return False
        if len(department.strip()) < 2:
            return False
        return True
        
    @staticmethod
    def validate_issue_description(description: str) -> bool:
        """Validate issue description"""
        if not description or not description.strip():
            return False
        if len(description.strip()) < 10:
            return False
        return True
        
    @staticmethod
    def validate_ticket_id(ticket_id: str) -> bool:
        """Validate ticket ID format"""
        if not ticket_id or not ticket_id.strip():
            return False
        return True
        
    @staticmethod
    def validate_menu_choice(choice: str, min_val: int, max_val: int) -> bool:
        """Validate menu choice"""
        try:
            choice_int = int(choice)
            return min_val <= choice_int <= max_val
        except ValueError:
            return False


class DebugHelper:
    """Debugging utilities"""
    
    @staticmethod
    def print_debug_info(message: str, variable: Any = None):
        """Print debug information"""
        print(f"[DEBUG] {message}")
        if variable is not None:
            print(f"[DEBUG] Variable: {variable}")
            print(f"[DEBUG] Type: {type(variable)}")
            
    @staticmethod
    def trace_function_call(func_name: str, args: tuple = (), kwargs: dict = None):
        """Trace function calls"""
        kwargs = kwargs or {}
        print(f"[TRACE] Calling {func_name}")
        if args:
            print(f"[TRACE] Args: {args}")
        if kwargs:
            print(f"[TRACE] Kwargs: {kwargs}")
            
    @staticmethod
    def log_error(error: Exception, context: str = ""):
        """Log detailed error information"""
        print(f"[ERROR] {context}" if context else "[ERROR]")
        print(f"[ERROR] Type: {type(error).__name__}")
        print(f"[ERROR] Message: {str(error)}")
        
        # Log to file as well
        logger = Logger()
        logger.log_event(f"Error in {context}: {type(error).__name__} - {str(error)}")


class ReportHelper:
    """Helper functions for report generation"""
    
    @staticmethod
    def format_datetime(datetime_str: str) -> str:
        """Format datetime string for display"""
        try:
            dt = datetime.strptime(datetime_str, "%Y-%m-%d %H:%M:%S")
            return dt.strftime("%d-%m-%Y %H:%M")
        except ValueError:
            return datetime_str
            
    @staticmethod
    def calculate_duration(start_time: str, end_time: str = None) -> str:
        """Calculate duration between two timestamps"""
        try:
            start = datetime.strptime(start_time, "%Y-%m-%d %H:%M:%S")
            end = datetime.strptime(end_time, "%Y-%m-%d %H:%M:%S") if end_time else datetime.now()
            
            duration = end - start
            
            if duration.days > 0:
                return f"{duration.days}d {duration.seconds//3600}h"
            elif duration.seconds >= 3600:
                return f"{duration.seconds//3600}h {(duration.seconds%3600)//60}m"
            else:
                return f"{duration.seconds//60}m"
                
        except ValueError:
            return "Unknown"
            
    @staticmethod
    def get_priority_color(priority: str) -> str:
        """Get color code for priority (for console output)"""
        colors = {
            "P1": "\033[91m",  # Red
            "P2": "\033[93m",  # Yellow
            "P3": "\033[94m",  # Blue
            "P4": "\033[92m"   # Green
        }
        return colors.get(priority, "")
        
    @staticmethod
    def reset_color() -> str:
        """Reset console color"""
        return "\033[0m"


class DataAnalyzer:
    """Analyze ticket data for insights"""
    
    @staticmethod
    def get_category_stats(tickets: List[Any]) -> Dict[str, int]:
        """Get statistics by category"""
        stats = {}
        for ticket in tickets:
            category = ticket.category
            stats[category] = stats.get(category, 0) + 1
        return stats
        
    @staticmethod
    def get_department_stats(tickets: List[Any]) -> Dict[str, int]:
        """Get statistics by department"""
        stats = {}
        for ticket in tickets:
            dept = ticket.department
            stats[dept] = stats.get(dept, 0) + 1
        return stats
        
    @staticmethod
    def get_monthly_stats(tickets: List[Any]) -> Dict[str, int]:
        """Get statistics by month"""
        stats = {}
        for ticket in tickets:
            try:
                dt = datetime.strptime(ticket.created_datetime, "%Y-%m-%d %H:%M:%S")
                month = dt.strftime("%Y-%m")
                stats[month] = stats.get(month, 0) + 1
            except ValueError:
                continue
        return stats
        
    @staticmethod
    def get_resolution_times(tickets: List[Any]) -> List[float]:
        """Calculate resolution times for closed tickets"""
        times = []
        for ticket in tickets:
            if ticket.status == "Closed":
                try:
                    created = datetime.strptime(ticket.created_datetime, "%Y-%m-%d %H:%M:%S")
                    updated = datetime.strptime(ticket.updated_datetime, "%Y-%m-%d %H:%M:%S")
                    duration_hours = (updated - created).total_seconds() / 3600
                    times.append(duration_hours)
                except ValueError:
                    continue
        return times
        
    @staticmethod
    def get_sla_breach_rate(tickets: List[Any]) -> float:
        """Calculate SLA breach rate"""
        if not tickets:
            return 0.0
            
        breached = len([t for t in tickets if t.sla_breached])
        return (breached / len(tickets)) * 100


def format_file_size(size_bytes: int) -> str:
    """Format file size in human readable format"""
    if size_bytes == 0:
        return "0 B"
        
    units = ["B", "KB", "MB", "GB"]
    unit_index = 0
    
    while size_bytes >= 1024 and unit_index < len(units) - 1:
        size_bytes /= 1024
        unit_index += 1
        
    return f"{size_bytes:.2f} {units[unit_index]}"


def validate_file_path(file_path: str) -> bool:
    """Validate if file path exists and is accessible"""
    return os.path.exists(file_path) and os.path.isfile(file_path)


def create_directory_if_not_exists(directory: str):
    """Create directory if it doesn't exist"""
    os.makedirs(directory, exist_ok=True)
