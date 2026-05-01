#!/usr/bin/env python3
"""
Smart IT Service Desk & System Monitoring Automation
Main Application Entry Point
"""

import os
import sys
from datetime import datetime
from tickets import TicketManager
from monitor import SystemMonitor
from reports import ReportGenerator
from utils import Logger, FileManager

class ITServiceDesk:
    """Main application class for IT Service Desk System"""
    
    def __init__(self):
        self.ticket_manager = TicketManager()
        self.system_monitor = SystemMonitor()
        self.report_generator = ReportGenerator()
        self.logger = Logger()
        self.file_manager = FileManager()
        
    def display_menu(self):
        """Display main menu options"""
        print("\n" + "="*60)
        print("     SMART IT SERVICE DESK SYSTEM")
        print("="*60)
        print("1. Create Ticket")
        print("2. View All Tickets")
        print("3. Search Ticket by ID")
        print("4. Update Ticket Status")
        print("5. Close Ticket")
        print("6. Delete Ticket")
        print("7. System Monitoring")
        print("8. Generate Reports")
        print("9. View Logs")
        print("10. Exit")
        print("="*60)
        
    def get_menu_choice(self):
        """Get and validate menu choice from user"""
        try:
            choice = int(input("\nEnter your choice (1-10): "))
            if choice < 1 or choice > 10:
                raise ValueError("Choice must be between 1 and 10")
            return choice
        except ValueError as e:
            print(f"Invalid input: {e}")
            return None
            
    def run(self):
        """Main application loop"""
        self.logger.log_event("Application started")
        
        while True:
            self.display_menu()
            choice = self.get_menu_choice()
            
            if choice is None:
                continue
                
            try:
                if choice == 1:
                    self.create_ticket()
                elif choice == 2:
                    self.view_all_tickets()
                elif choice == 3:
                    self.search_ticket()
                elif choice == 4:
                    self.update_ticket_status()
                elif choice == 5:
                    self.close_ticket()
                elif choice == 6:
                    self.delete_ticket()
                elif choice == 7:
                    self.system_monitoring()
                elif choice == 8:
                    self.generate_reports()
                elif choice == 9:
                    self.view_logs()
                elif choice == 10:
                    print("\nThank you for using IT Service Desk System!")
                    self.logger.log_event("Application closed")
                    break
                    
            except KeyboardInterrupt:
                print("\nOperation cancelled by user.")
                self.logger.log_event("Operation cancelled by user")
            except Exception as e:
                print(f"Error occurred: {e}")
                self.logger.log_event(f"Error: {e}")
                
    def create_ticket(self):
        """Create a new support ticket"""
        print("\n--- Create New Ticket ---")
        
        try:
            employee_name = input("Employee Name: ").strip()
            if not employee_name:
                raise ValueError("Employee name cannot be empty")
                
            department = input("Department: ").strip()
            if not department:
                raise ValueError("Department cannot be empty")
                
            print("\nIssue Categories:")
            categories = ["Server Down", "Internet Down", "Laptop Slow", "Password Reset", 
                         "Printer Not Working", "Outlook Not Opening", "Application Crash", "Other"]
            for i, cat in enumerate(categories, 1):
                print(f"{i}. {cat}")
                
            cat_choice = int(input("Select category (1-8): "))
            if cat_choice < 1 or cat_choice > 8:
                raise ValueError("Invalid category selection")
                
            category = categories[cat_choice - 1]
            issue_description = input("Issue Description: ").strip()
            if not issue_description:
                raise ValueError("Issue description cannot be empty")
                
            ticket = self.ticket_manager.create_ticket(
                employee_name, department, category, issue_description
            )
            
            print(f"\nTicket created successfully!")
            print(f"Ticket ID: {ticket.ticket_id}")
            print(f"Priority: {ticket.priority}")
            print(f"Status: {ticket.status}")
            
            self.logger.log_event(f"Ticket created: {ticket.ticket_id}")
            
        except ValueError as e:
            print(f"Error: {e}")
        except Exception as e:
            print(f"Unexpected error: {e}")
            self.logger.log_event(f"Error creating ticket: {e}")
            
    def view_all_tickets(self):
        """Display all tickets"""
        tickets = self.ticket_manager.get_all_tickets()
        
        if not tickets:
            print("\nNo tickets found.")
            return
            
        print("\n--- All Tickets ---")
        for ticket in tickets:
            print(f"\nID: {ticket.ticket_id}")
            print(f"Employee: {ticket.employee_name}")
            print(f"Department: {ticket.department}")
            print(f"Category: {ticket.category}")
            print(f"Priority: {ticket.priority}")
            print(f"Status: {ticket.status}")
            print(f"Created: {ticket.created_datetime}")
            print(f"Description: {ticket.issue_description[:50]}...")
            print("-" * 40)
            
    def search_ticket(self):
        """Search for a specific ticket by ID"""
        try:
            ticket_id = input("Enter Ticket ID: ").strip()
            if not ticket_id:
                raise ValueError("Ticket ID cannot be empty")
                
            ticket = self.ticket_manager.get_ticket_by_id(ticket_id)
            
            if ticket:
                print("\n--- Ticket Details ---")
                print(f"ID: {ticket.ticket_id}")
                print(f"Employee: {ticket.employee_name}")
                print(f"Department: {ticket.department}")
                print(f"Category: {ticket.category}")
                print(f"Priority: {ticket.priority}")
                print(f"Status: {ticket.status}")
                print(f"Created: {ticket.created_datetime}")
                print(f"Updated: {ticket.updated_datetime}")
                print(f"Description: {ticket.issue_description}")
            else:
                print(f"Ticket with ID '{ticket_id}' not found.")
                
        except ValueError as e:
            print(f"Error: {e}")
        except Exception as e:
            print(f"Unexpected error: {e}")
            
    def update_ticket_status(self):
        """Update ticket status"""
        try:
            ticket_id = input("Enter Ticket ID: ").strip()
            if not ticket_id:
                raise ValueError("Ticket ID cannot be empty")
                
            ticket = self.ticket_manager.get_ticket_by_id(ticket_id)
            if not ticket:
                print(f"Ticket with ID '{ticket_id}' not found.")
                return
                
            print(f"\nCurrent Status: {ticket.status}")
            print("\nAvailable Statuses:")
            statuses = ["Open", "In Progress", "Closed"]
            for i, status in enumerate(statuses, 1):
                print(f"{i}. {status}")
                
            status_choice = int(input("Select new status (1-3): "))
            if status_choice < 1 or status_choice > 3:
                raise ValueError("Invalid status selection")
                
            new_status = statuses[status_choice - 1]
            
            if self.ticket_manager.update_ticket_status(ticket_id, new_status):
                print(f"Ticket status updated to: {new_status}")
                self.logger.log_event(f"Ticket {ticket_id} status updated to {new_status}")
            else:
                print("Failed to update ticket status.")
                
        except ValueError as e:
            print(f"Error: {e}")
        except Exception as e:
            print(f"Unexpected error: {e}")
            
    def close_ticket(self):
        """Close a ticket"""
        try:
            ticket_id = input("Enter Ticket ID: ").strip()
            if not ticket_id:
                raise ValueError("Ticket ID cannot be empty")
                
            if self.ticket_manager.close_ticket(ticket_id):
                print(f"Ticket {ticket_id} closed successfully.")
                self.logger.log_event(f"Ticket {ticket_id} closed")
            else:
                print(f"Failed to close ticket {ticket_id}.")
                
        except ValueError as e:
            print(f"Error: {e}")
        except Exception as e:
            print(f"Unexpected error: {e}")
            
    def delete_ticket(self):
        """Delete a ticket"""
        try:
            ticket_id = input("Enter Ticket ID: ").strip()
            if not ticket_id:
                raise ValueError("Ticket ID cannot be empty")
                
            confirm = input(f"Are you sure you want to delete ticket {ticket_id}? (y/N): ").strip().lower()
            if confirm != 'y':
                print("Deletion cancelled.")
                return
                
            if self.ticket_manager.delete_ticket(ticket_id):
                print(f"Ticket {ticket_id} deleted successfully.")
                self.logger.log_event(f"Ticket {ticket_id} deleted")
            else:
                print(f"Failed to delete ticket {ticket_id}.")
                
        except ValueError as e:
            print(f"Error: {e}")
        except Exception as e:
            print(f"Unexpected error: {e}")
            
    def system_monitoring(self):
        """Run system monitoring"""
        print("\n--- System Monitoring ---")
        self.system_monitor.check_system_health()
        
    def generate_reports(self):
        """Generate various reports"""
        print("\n--- Generate Reports ---")
        print("1. Daily Summary Report")
        print("2. Monthly Trend Report")
        
        try:
            choice = int(input("Select report type (1-2): "))
            if choice == 1:
                self.report_generator.generate_daily_report()
            elif choice == 2:
                self.report_generator.generate_monthly_report()
            else:
                print("Invalid choice.")
        except ValueError:
            print("Invalid input.")
            
    def view_logs(self):
        """View system logs"""
        print("\n--- System Logs ---")
        self.logger.display_logs()


if __name__ == "__main__":
    app = ITServiceDesk()
    app.run()
