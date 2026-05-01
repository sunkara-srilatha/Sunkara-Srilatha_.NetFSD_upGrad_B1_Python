"""
Report Generation System
Generates daily summary and monthly trend reports
"""

import os
from datetime import datetime, timedelta
from typing import Dict, List
from collections import Counter
from tickets import TicketManager, Ticket
from utils import Logger, DataAnalyzer, ReportHelper

class ReportGenerator:
    """Generates various types of reports"""
    
    def __init__(self):
        self.ticket_manager = TicketManager()
        self.logger = Logger()
        self.data_analyzer = DataAnalyzer()
        self.report_helper = ReportHelper()
        
    def generate_daily_report(self):
        """Generate daily summary report"""
        print("\n--- Daily Summary Report ---")
        
        # Get today's tickets
        today = datetime.now().strftime("%Y-%m-%d")
        today_tickets = self.get_tickets_by_date(today)
        
        # Get all tickets for statistics
        all_tickets = self.ticket_manager.get_all_tickets()
        stats = self.ticket_manager.get_statistics()
        
        # Display report header
        print(f"\nDate: {datetime.now().strftime('%d-%m-%Y')}")
        print("="*60)
        
        # Summary Statistics
        print(f"SUMMARY STATISTICS")
        print(f"Total Tickets Raised: {stats['total_tickets']}")
        print(f"Open Tickets: {stats['open_tickets']}")
        print(f"In Progress: {stats['in_progress']}")
        print(f"Closed Tickets: {stats['closed_tickets']}")
        print(f"High Priority Tickets: {stats['high_priority']}")
        print(f"SLA Breached: {stats['sla_breached']}")
        
        # Today's Activity
        print(f"\nTODAY'S ACTIVITY")
        print(f"New Tickets Today: {len(today_tickets)}")
        
        if today_tickets:
            print(f"Categories Today:")
            category_stats = self.data_analyzer.get_category_stats(today_tickets)
            for category, count in category_stats.items():
                print(f"  {category}: {count}")
                
        # Priority Distribution
        print(f"\nPRIORITY DISTRIBUTION")
        priority_stats = self.get_priority_distribution(all_tickets)
        for priority, count in priority_stats.items():
            color = self.report_helper.get_priority_color(priority)
            reset = self.report_helper.reset_color()
            print(f"{color}{priority}: {count}{reset}")
            
        # Department Statistics
        print(f"\nDEPARTMENT STATISTICS")
        dept_stats = self.data_analyzer.get_department_stats(all_tickets)
        for dept, count in dept_stats.items():
            print(f"{dept}: {count}")
            
        # SLA Analysis
        print(f"\nSLA ANALYSIS")
        breach_rate = self.data_analyzer.get_sla_breach_rate(all_tickets)
        print(f"SLA Breach Rate: {breach_rate:.1f}%")
        
        # Escalation Alerts
        alerts = self.ticket_manager.get_escalation_alerts()
        if alerts:
            print(f"\nESCALATION ALERTS ({len(alerts)})")
            for alert in alerts[:5]:  # Show top 5
                print(f"  {alert['ticket_id']} - {alert['reason']}")
        else:
            print(f"\nNo escalation alerts.")
            
        # Top Issues Today
        if today_tickets:
            print(f"\nTOP ISSUES TODAY")
            category_stats = self.data_analyzer.get_category_stats(today_tickets)
            sorted_categories = sorted(category_stats.items(), key=lambda x: x[1], reverse=True)
            for category, count in sorted_categories[:3]:
                print(f"{category}: {count}")
                
        # Save report to file
        self.save_daily_report(stats, today_tickets, alerts)
        
        print(f"\nDaily report saved to data/daily_report_{today.replace('-', '')}.txt")
        
    def generate_monthly_report(self):
        """Generate monthly trend report"""
        print("\n--- Monthly Trend Report ---")
        
        # Get current month
        now = datetime.now()
        current_month = now.strftime("%Y-%m")
        month_name = now.strftime("%B %Y")
        
        # Get all tickets
        all_tickets = self.ticket_manager.get_all_tickets()
        
        # Filter tickets for current month
        month_tickets = self.get_tickets_by_month(current_month, all_tickets)
        
        print(f"\nMonth: {month_name}")
        print("="*60)
        
        # Monthly Statistics
        print(f"MONTHLY STATISTICS")
        print(f"Total Tickets: {len(month_tickets)}")
        
        if month_tickets:
            # Most Common Issues
            print(f"\nMOST COMMON ISSUES")
            category_stats = self.data_analyzer.get_category_stats(month_tickets)
            sorted_categories = sorted(category_stats.items(), key=lambda x: x[1], reverse=True)
            
            for i, (category, count) in enumerate(sorted_categories[:5], 1):
                percentage = (count / len(month_tickets)) * 100
                print(f"{i}. {category}: {count} ({percentage:.1f}%)")
                
            # Department Analysis
            print(f"\nDEPARTMENT ANALYSIS")
            dept_stats = self.data_analyzer.get_department_stats(month_tickets)
            sorted_depts = sorted(dept_stats.items(), key=lambda x: x[1], reverse=True)
            
            for dept, count in sorted_depts:
                percentage = (count / len(month_tickets)) * 100
                print(f"{dept}: {count} ({percentage:.1f}%)")
                
            # Average Resolution Time
            print(f"\nRESOLUTION ANALYSIS")
            resolution_times = self.data_analyzer.get_resolution_times(month_tickets)
            if resolution_times:
                avg_time = sum(resolution_times) / len(resolution_times)
                print(f"Average Resolution Time: {avg_time:.1f} hours")
                print(f"Fastest Resolution: {min(resolution_times):.1f} hours")
                print(f"Slowest Resolution: {max(resolution_times):.1f} hours")
            else:
                print("No closed tickets for resolution analysis.")
                
            # Priority Analysis
            print(f"\nPRIORITY ANALYSIS")
            priority_stats = self.get_priority_distribution(month_tickets)
            for priority, count in priority_stats.items():
                color = self.report_helper.get_priority_color(priority)
                reset = self.report_helper.reset_color()
                percentage = (count / len(month_tickets)) * 100
                print(f"{color}{priority}: {count} ({percentage:.1f}%){reset}")
                
            # SLA Performance
            print(f"\nSLA PERFORMANCE")
            breach_rate = self.data_analyzer.get_sla_breach_rate(month_tickets)
            print(f"SLA Breach Rate: {breach_rate:.1f}%")
            
            # Weekly Trends
            print(f"\nWEEKLY TRENDS")
            weekly_stats = self.get_weekly_stats(month_tickets)
            for week, count in weekly_stats.items():
                print(f"Week {week}: {count} tickets")
                
            # Problem Records
            problem_records = self.ticket_manager.problem_records
            if problem_records:
                print(f"\nPROBLEM RECORDS")
                print(f"Active Problems: {len(problem_records)}")
                for problem in problem_records[:3]:
                    print(f"  {problem.problem_id}: {problem.problem_description[:50]}...")
                    
        else:
            print("No tickets found for this month.")
            
        # Save monthly report
        self.save_monthly_report(month_tickets, current_month)
        
        print(f"\nMonthly report saved to data/monthly_report_{current_month}.txt")
        
    def get_tickets_by_date(self, date: str) -> List[Ticket]:
        """Get tickets for a specific date"""
        tickets = []
        for ticket in self.ticket_manager.get_all_tickets():
            try:
                ticket_date = ticket.created_datetime.split(' ')[0]
                if ticket_date == date:
                    tickets.append(ticket)
            except (ValueError, IndexError):
                continue
        return tickets
        
    def get_tickets_by_month(self, month: str, tickets: List[Ticket]) -> List[Ticket]:
        """Get tickets for a specific month (YYYY-MM format)"""
        month_tickets = []
        for ticket in tickets:
            try:
                ticket_month = ticket.created_datetime[:7]  # YYYY-MM
                if ticket_month == month:
                    month_tickets.append(ticket)
            except (ValueError, IndexError):
                continue
        return month_tickets
        
    def get_priority_distribution(self, tickets: List[Ticket]) -> Dict[str, int]:
        """Get distribution of tickets by priority"""
        priorities = ["P1", "P2", "P3", "P4"]
        distribution = {p: 0 for p in priorities}
        
        for ticket in tickets:
            if ticket.priority in distribution:
                distribution[ticket.priority] += 1
                
        return distribution
        
    def get_weekly_stats(self, tickets: List[Ticket]) -> Dict[str, int]:
        """Get weekly ticket statistics"""
        weekly_stats = {}
        
        for ticket in tickets:
            try:
                created_date = datetime.strptime(ticket.created_datetime, "%Y-%m-%d %H:%M:%S")
                week_number = created_date.isocalendar()[1]
                week_key = str(week_number)
                weekly_stats[week_key] = weekly_stats.get(week_key, 0) + 1
            except ValueError:
                continue
                
        return weekly_stats
        
    def save_daily_report(self, stats: Dict, today_tickets: List[Ticket], alerts: List[Dict]):
        """Save daily report to file"""
        try:
            os.makedirs("data", exist_ok=True)
            filename = f"data/daily_report_{datetime.now().strftime('%Y%m%d')}.txt"
            
            with open(filename, 'w', encoding='utf-8') as f:
                f.write(f"DAILY SUMMARY REPORT\n")
                f.write(f"Date: {datetime.now().strftime('%d-%m-%Y')}\n")
                f.write(f"Generated: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}\n")
                f.write("="*60 + "\n\n")
                
                f.write("SUMMARY STATISTICS\n")
                f.write(f"Total Tickets Raised: {stats['total_tickets']}\n")
                f.write(f"Open Tickets: {stats['open_tickets']}\n")
                f.write(f"In Progress: {stats['in_progress']}\n")
                f.write(f"Closed Tickets: {stats['closed_tickets']}\n")
                f.write(f"High Priority Tickets: {stats['high_priority']}\n")
                f.write(f"SLA Breached: {stats['sla_breached']}\n\n")
                
                f.write(f"TODAY'S ACTIVITY\n")
                f.write(f"New Tickets Today: {len(today_tickets)}\n\n")
                
                if today_tickets:
                    f.write("Categories Today:\n")
                    category_stats = self.data_analyzer.get_category_stats(today_tickets)
                    for category, count in category_stats.items():
                        f.write(f"  {category}: {count}\n")
                    f.write("\n")
                    
                f.write("ESCALATION ALERTS\n")
                f.write(f"Total Alerts: {len(alerts)}\n")
                for alert in alerts:
                    f.write(f"  {alert['ticket_id']} - {alert['reason']}\n")
                    
        except Exception as e:
            self.logger.log_event(f"Error saving daily report: {e}")
            
    def save_monthly_report(self, month_tickets: List[Ticket], month: str):
        """Save monthly report to file"""
        try:
            os.makedirs("data", exist_ok=True)
            filename = f"data/monthly_report_{month}.txt"
            
            with open(filename, 'w', encoding='utf-8') as f:
                f.write(f"MONTHLY TREND REPORT\n")
                f.write(f"Month: {datetime.strptime(month, '%Y-%m').strftime('%B %Y')}\n")
                f.write(f"Generated: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}\n")
                f.write("="*60 + "\n\n")
                
                f.write(f"MONTHLY STATISTICS\n")
                f.write(f"Total Tickets: {len(month_tickets)}\n\n")
                
                if month_tickets:
                    f.write("MOST COMMON ISSUES\n")
                    category_stats = self.data_analyzer.get_category_stats(month_tickets)
                    sorted_categories = sorted(category_stats.items(), key=lambda x: x[1], reverse=True)
                    
                    for i, (category, count) in enumerate(sorted_categories, 1):
                        percentage = (count / len(month_tickets)) * 100
                        f.write(f"{i}. {category}: {count} ({percentage:.1f}%)\n")
                    f.write("\n")
                    
                    f.write("DEPARTMENT ANALYSIS\n")
                    dept_stats = self.data_analyzer.get_department_stats(month_tickets)
                    sorted_depts = sorted(dept_stats.items(), key=lambda x: x[1], reverse=True)
                    
                    for dept, count in sorted_depts:
                        percentage = (count / len(month_tickets)) * 100
                        f.write(f"{dept}: {count} ({percentage:.1f}%)\n")
                    f.write("\n")
                    
                    f.write("RESOLUTION ANALYSIS\n")
                    resolution_times = self.data_analyzer.get_resolution_times(month_tickets)
                    if resolution_times:
                        avg_time = sum(resolution_times) / len(resolution_times)
                        f.write(f"Average Resolution Time: {avg_time:.1f} hours\n")
                        f.write(f"Fastest Resolution: {min(resolution_times):.1f} hours\n")
                        f.write(f"Slowest Resolution: {max(resolution_times):.1f} hours\n")
                    f.write("\n")
                    
        except Exception as e:
            self.logger.log_event(f"Error saving monthly report: {e}")
            
    def generate_custom_report(self, start_date: str, end_date: str):
        """Generate custom date range report"""
        print(f"\n--- Custom Report: {start_date} to {end_date} ---")
        
        # Convert dates
        try:
            start = datetime.strptime(start_date, "%Y-%m-%d")
            end = datetime.strptime(end_date, "%Y-%m-%d")
        except ValueError:
            print("Invalid date format. Use YYYY-MM-DD")
            return
            
        # Filter tickets by date range
        filtered_tickets = []
        for ticket in self.ticket_manager.get_all_tickets():
            try:
                ticket_date = datetime.strptime(ticket.created_datetime, "%Y-%m-%d %H:%M:%S")
                if start <= ticket_date <= end:
                    filtered_tickets.append(ticket)
            except ValueError:
                continue
                
        print(f"Tickets in range: {len(filtered_tickets)}")
        
        if filtered_tickets:
            # Generate similar statistics as monthly report
            print(f"\nMost Common Issues:")
            category_stats = self.data_analyzer.get_category_stats(filtered_tickets)
            sorted_categories = sorted(category_stats.items(), key=lambda x: x[1], reverse=True)
            
            for category, count in sorted_categories[:5]:
                print(f"  {category}: {count}")
                
            print(f"\nDepartment Analysis:")
            dept_stats = self.data_analyzer.get_department_stats(filtered_tickets)
            for dept, count in dept_stats.items():
                print(f"  {dept}: {count}")
                
        else:
            print("No tickets found in the specified date range.")
            
    def export_to_csv(self, filename: str = None):
        """Export all tickets to CSV file"""
        if not filename:
            filename = f"data/ticket_export_{datetime.now().strftime('%Y%m%d_%H%M%S')}.csv"
            
        try:
            import csv
            
            tickets = self.ticket_manager.get_all_tickets()
            
            with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
                fieldnames = ['ticket_id', 'employee_name', 'department', 'category',
                             'issue_description', 'priority', 'status', 'created_datetime',
                             'updated_datetime', 'sla_deadline', 'sla_breached']
                writer = csv.DictWriter(csvfile, fieldnames=fieldnames)
                
                writer.writeheader()
                for ticket in tickets:
                    writer.writerow(ticket.to_dict())
                    
            print(f"Tickets exported to: {filename}")
            self.logger.log_event(f"Tickets exported to {filename}")
            
        except Exception as e:
            print(f"Error exporting tickets: {e}")
            self.logger.log_event(f"Error exporting tickets: {e}")
