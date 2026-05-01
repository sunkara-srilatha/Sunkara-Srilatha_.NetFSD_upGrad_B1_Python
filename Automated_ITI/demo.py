#!/usr/bin/env python3
"""
Demo script for Smart IT Service Desk System
Demonstrates key features and functionality
"""

from tickets import TicketManager, IncidentTicket, ServiceRequest
from monitor import SystemMonitor
from reports import ReportGenerator
from utils import Logger
import time

def demo_ticket_management():
    """Demonstrate ticket management features"""
    print("\n=== TICKET MANAGEMENT DEMO ===")
    
    # Create ticket manager
    tm = TicketManager()
    
    # Create sample tickets
    print("\n1. Creating sample tickets...")
    
    # Incident ticket
    incident = tm.create_ticket(
        "John Smith", "IT", "Server Down", 
        "Main application server is not responding"
    )
    print(f"   Created incident ticket: {incident.ticket_id} (Priority: {incident.priority})")
    
    # Service request
    service = tm.create_ticket(
        "Jane Doe", "HR", "Password Reset",
        "Need to reset network password for new employee"
    )
    print(f"   Created service request: {service.ticket_id} (Priority: {service.priority})")
    
    # View all tickets
    print(f"\n2. Total tickets in system: {len(tm.get_all_tickets())}")
    
    # Update ticket status
    print(f"\n3. Updating ticket status...")
    tm.update_ticket_status(incident.ticket_id, "In Progress")
    updated_ticket = tm.get_ticket_by_id(incident.ticket_id)
    print(f"   Ticket {incident.ticket_id} status: {updated_ticket.status}")
    
    # Check SLA
    print(f"\n4. SLA Information:")
    print(f"   {incident.ticket_id} SLA Deadline: {incident.sla_deadline}")
    print(f"   SLA Breached: {incident.check_sla_breach()}")
    
    return tm

def demo_system_monitoring():
    """Demonstrate system monitoring"""
    print("\n=== SYSTEM MONITORING DEMO ===")
    
    monitor = SystemMonitor()
    
    print("\n1. Getting system metrics...")
    cpu_usage = monitor.get_cpu_usage()
    ram_info = monitor.get_ram_usage()
    disk_info = monitor.get_disk_usage()
    
    print(f"   CPU Usage: {cpu_usage:.1f}%")
    print(f"   RAM Usage: {ram_info['percentage']:.1f}%")
    print(f"   Disk Drives: {len(disk_info)}")
    
    print("\n2. Checking thresholds...")
    alerts = monitor.check_thresholds()
    if alerts:
        print(f"   Alerts found: {len(alerts)}")
        for alert_type, alert_data in alerts.items():
            print(f"   - {alert_type}: {alert_data['current']:.1f}% (Threshold: {alert_data['threshold']:.1f}%)")
    else:
        print("   All systems normal")
    
    return monitor

def demo_reporting(tm):
    """Demonstrate reporting features"""
    print("\n=== REPORTING DEMO ===")
    
    rg = ReportGenerator()
    
    print("\n1. Getting ticket statistics...")
    stats = tm.get_statistics()
    print(f"   Total Tickets: {stats['total_tickets']}")
    print(f"   Open Tickets: {stats['open_tickets']}")
    print(f"   High Priority: {stats['high_priority']}")
    
    print("\n2. Checking escalation alerts...")
    alerts = tm.get_escalation_alerts()
    print(f"   Escalation Alerts: {len(alerts)}")
    
    return rg

def demo_logging():
    """Demonstrate logging functionality"""
    print("\n=== LOGGING DEMO ===")
    
    logger = Logger()
    
    print("\n1. Creating sample log entries...")
    logger.log_event("Demo started")
    logger.log_event("Sample ticket created")
    logger.log_event("System health check completed")
    
    print("\n2. Getting log statistics...")
    stats = logger.get_log_stats()
    print(f"   Total log entries: {stats['total_entries']}")
    print(f"   Log file size: {stats['file_size']} bytes")

def main():
    """Run complete demo"""
    print("SMART IT SERVICE DESK SYSTEM DEMO")
    print("="*50)
    
    try:
        # Demo ticket management
        tm = demo_ticket_management()
        time.sleep(1)
        
        # Demo system monitoring
        monitor = demo_system_monitoring()
        time.sleep(1)
        
        # Demo reporting
        rg = demo_reporting(tm)
        time.sleep(1)
        
        # Demo logging
        demo_logging()
        
        print("\n" + "="*50)
        print("DEMO COMPLETED SUCCESSFULLY!")
        print("All major components are working correctly.")
        print("Run 'python main.py' to start the full application.")
        
    except Exception as e:
        print(f"\nDemo error: {e}")
        import traceback
        traceback.print_exc()

if __name__ == "__main__":
    main()
