"""
System Monitoring Module
Monitors CPU, RAM, and Disk usage with automatic ticket generation
"""

import psutil
import platform
from datetime import datetime
from typing import Dict, List
from tickets import TicketManager, IncidentTicket
from utils import Logger

class SystemMonitor:
    """Monitors system health and generates alerts"""
    
    def __init__(self):
        self.logger = Logger()
        self.ticket_manager = TicketManager()
        self.thresholds = {
            "cpu": 90.0,      # CPU usage threshold in %
            "ram": 95.0,      # RAM usage threshold in %
            "disk": 10.0      # Minimum free disk space in %
        }
        
    def get_cpu_usage(self) -> float:
        """Get current CPU usage percentage"""
        try:
            return psutil.cpu_percent(interval=1)
        except Exception as e:
            self.logger.log_event(f"Error getting CPU usage: {e}")
            return 0.0
            
    def get_ram_usage(self) -> Dict:
        """Get RAM usage information"""
        try:
            memory = psutil.virtual_memory()
            return {
                "total": memory.total,
                "available": memory.available,
                "used": memory.used,
                "percentage": memory.percent
            }
        except Exception as e:
            self.logger.log_event(f"Error getting RAM usage: {e}")
            return {"percentage": 0.0}
            
    def get_disk_usage(self) -> List[Dict]:
        """Get disk usage information for all drives"""
        disk_info = []
        try:
            partitions = psutil.disk_partitions()
            for partition in partitions:
                try:
                    usage = psutil.disk_usage(partition.mountpoint)
                    free_percentage = (usage.free / usage.total) * 100
                    
                    disk_info.append({
                        "device": partition.device,
                        "mountpoint": partition.mountpoint,
                        "total": usage.total,
                        "used": usage.used,
                        "free": usage.free,
                        "percentage_used": (usage.used / usage.total) * 100,
                        "percentage_free": free_percentage
                    })
                except PermissionError:
                    # Skip drives we don't have permission to access
                    continue
        except Exception as e:
            self.logger.log_event(f"Error getting disk usage: {e}")
            
        return disk_info
        
    def get_system_info(self) -> Dict:
        """Get general system information"""
        try:
            return {
                "system": platform.system(),
                "node": platform.node(),
                "release": platform.release(),
                "version": platform.version(),
                "machine": platform.machine(),
                "processor": platform.processor()
            }
        except Exception as e:
            self.logger.log_event(f"Error getting system info: {e}")
            return {}
            
    def check_thresholds(self) -> Dict:
        """Check if any thresholds are exceeded"""
        alerts = {}
        
        # Check CPU usage
        cpu_usage = self.get_cpu_usage()
        if cpu_usage > self.thresholds["cpu"]:
            alerts["cpu"] = {
                "current": cpu_usage,
                "threshold": self.thresholds["cpu"],
                "status": "CRITICAL"
            }
            
        # Check RAM usage
        ram_info = self.get_ram_usage()
        if ram_info["percentage"] > self.thresholds["ram"]:
            alerts["ram"] = {
                "current": ram_info["percentage"],
                "threshold": self.thresholds["ram"],
                "status": "CRITICAL"
            }
            
        # Check disk usage
        disk_info = self.get_disk_usage()
        for disk in disk_info:
            if disk["percentage_free"] < self.thresholds["disk"]:
                alerts[f"disk_{disk['device']}"] = {
                    "current": disk["percentage_free"],
                    "threshold": self.thresholds["disk"],
                    "status": "CRITICAL",
                    "device": disk["device"]
                }
                
        return alerts
        
    def generate_monitoring_ticket(self, alert_type: str, alert_data: Dict) -> str:
        """Generate a high-priority ticket for system alerts"""
        try:
            employee_name = "System Monitor"
            department = "IT Infrastructure"
            
            if alert_type == "cpu":
                category = "Server Down"
                issue_description = f"CPU usage is {alert_data['current']:.1f}%, exceeding threshold of {alert_data['threshold']:.1f}%"
            elif alert_type == "ram":
                category = "Server Down" 
                issue_description = f"RAM usage is {alert_data['current']:.1f}%, exceeding threshold of {alert_data['threshold']:.1f}%"
            else:  # disk
                category = "Server Down"
                device = alert_data.get("device", "Unknown")
                issue_description = f"Disk {device} free space is {alert_data['current']:.1f}%, below threshold of {alert_data['threshold']:.1f}%"
                
            ticket = IncidentTicket(employee_name, department, category, issue_description, "High")
            self.ticket_manager.tickets.append(ticket)
            self.ticket_manager.save_tickets()
            
            self.logger.log_event(f"System monitoring ticket created: {ticket.ticket_id}")
            return ticket.ticket_id
            
        except Exception as e:
            self.logger.log_event(f"Error creating monitoring ticket: {e}")
            return ""
            
    def check_system_health(self) -> Dict:
        """Perform complete system health check"""
        print("Performing system health check...")
        
        # Get system metrics
        cpu_usage = self.get_cpu_usage()
        ram_info = self.get_ram_usage()
        disk_info = self.get_disk_usage()
        system_info = get_system_info()
        
        # Display results
        print(f"\nSystem Information:")
        print(f"System: {system_info.get('system', 'Unknown')}")
        print(f"Node: {system_info.get('node', 'Unknown')}")
        print(f"Processor: {system_info.get('processor', 'Unknown')}")
        
        print(f"\nCPU Usage: {cpu_usage:.1f}%")
        if cpu_usage > self.thresholds["cpu"]:
            print("  Status: CRITICAL - Threshold exceeded!")
        else:
            print("  Status: Normal")
            
        print(f"\nMemory Usage: {ram_info['percentage']:.1f}%")
        print(f"  Total: {self.format_bytes(ram_info.get('total', 0))}")
        print(f"  Used: {self.format_bytes(ram_info.get('used', 0))}")
        print(f"  Available: {self.format_bytes(ram_info.get('available', 0))}")
        if ram_info["percentage"] > self.thresholds["ram"]:
            print("  Status: CRITICAL - Threshold exceeded!")
        else:
            print("  Status: Normal")
            
        print(f"\nDisk Usage:")
        for disk in disk_info:
            print(f"  Device: {disk['device']}")
            print(f"    Total: {self.format_bytes(disk['total'])}")
            print(f"    Used: {self.format_bytes(disk['used'])}")
            print(f"    Free: {self.format_bytes(disk['free'])}")
            print(f"    Usage: {disk['percentage_used']:.1f}%")
            print(f"    Free Space: {disk['percentage_free']:.1f}%")
            if disk["percentage_free"] < self.thresholds["disk"]:
                print("    Status: CRITICAL - Threshold exceeded!")
            else:
                print("    Status: Normal")
            print()
            
        # Check for alerts and generate tickets
        alerts = self.check_thresholds()
        generated_tickets = []
        
        if alerts:
            print("\nALERTS DETECTED:")
            for alert_type, alert_data in alerts.items():
                print(f"  {alert_type.upper()}: {alert_data['current']:.1f}% (Threshold: {alert_data['threshold']:.1f}%)")
                
                # Generate ticket for critical alerts
                ticket_id = self.generate_monitoring_ticket(alert_type, alert_data)
                if ticket_id:
                    generated_tickets.append(ticket_id)
                    print(f"  -> High-priority ticket generated: {ticket_id}")
                    
            if generated_tickets:
                print(f"\nGenerated {len(generated_tickets)} monitoring ticket(s)")
        else:
            print("\nAll systems operating within normal parameters.")
            
        # Log the health check
        self.logger.log_event(f"System health check completed - Alerts: {len(alerts)}")
        
        return {
            "cpu_usage": cpu_usage,
            "ram_info": ram_info,
            "disk_info": disk_info,
            "system_info": system_info,
            "alerts": alerts,
            "generated_tickets": generated_tickets
        }
        
    def format_bytes(self, bytes_value: int) -> str:
        if bytes_value == 0:
            return "0 B"

        units = ["B", "KB", "MB", "GB", "TB"]
        i = 0

        while bytes_value >= 1024 and i < len(units) - 1:
            bytes_value /= 1024
            i += 1

        return f"{bytes_value:.2f} {units[i]}"
            
    def start_monitoring(self, interval_minutes: int = 5):
        """Start continuous monitoring (runs in background)"""
        import threading
        import time
        
        def monitor_loop():
            while True:
                try:
                    self.check_system_health()
                    time.sleep(interval_minutes * 60)
                except KeyboardInterrupt:
                    print("\nMonitoring stopped by user.")
                    break
                except Exception as e:
                    self.logger.log_event(f"Error in monitoring loop: {e}")
                    time.sleep(interval_minutes * 60)
                    
        print(f"Starting system monitoring (every {interval_minutes} minutes)...")
        print("Press Ctrl+C to stop monitoring.")
        
        monitor_thread = threading.Thread(target=monitor_loop, daemon=True)
        monitor_thread.start()
        
        try:
            monitor_thread.join()
        except KeyboardInterrupt:
            print("\nMonitoring stopped.")
            
    def get_monitoring_report(self) -> str:
        """Generate a formatted monitoring report"""
        cpu_usage = self.get_cpu_usage()
        ram_info = self.get_ram_usage()
        disk_info = self.get_disk_usage()
        alerts = self.check_thresholds()
        
        report = f"\n{'='*50}\n"
        report += f"SYSTEM MONITORING REPORT\n"
        report += f"Generated: {datetime.now().strftime('%Y-%m-%d %H:%M:%S')}\n"
        report += f"{'='*50}\n\n"
        
        report += f"CPU Usage: {cpu_usage:.1f}%\n"
        report += f"Status: {'CRITICAL' if cpu_usage > self.thresholds['cpu'] else 'NORMAL'}\n\n"
        
        report += f"Memory Usage: {ram_info['percentage']:.1f}%\n"
        report += f"Total: {self.format_bytes(ram_info.get('total', 0))}\n"
        report += f"Available: {self.format_bytes(ram_info.get('available', 0))}\n"
        report += f"Status: {'CRITICAL' if ram_info['percentage'] > self.thresholds['ram'] else 'NORMAL'}\n\n"
        
        report += f"Disk Usage:\n"
        for disk in disk_info:
            report += f"  {disk['device']}: {disk['percentage_used']:.1f}% used, {disk['percentage_free']:.1f}% free\n"
            report += f"    Status: {'CRITICAL' if disk['percentage_free'] < self.thresholds['disk'] else 'NORMAL'}\n"
            
        if alerts:
            report += f"\nALERTS ({len(alerts)}):\n"
            for alert_type, alert_data in alerts.items():
                report += f"  {alert_type.upper()}: {alert_data['current']:.1f}% (Threshold: {alert_data['threshold']:.1f}%)\n"
        else:
            report += f"\nNo alerts detected.\n"
            
        report += f"{'='*50}\n"
        
        return report


def get_system_info() -> Dict:
    """Get system information (standalone function)"""
    try:
        return {
            "system": platform.system(),
            "node": platform.node(),
            "release": platform.release(),
            "version": platform.version(),
            "machine": platform.machine(),
            "processor": platform.processor()
        }
    except Exception:
        return {}


