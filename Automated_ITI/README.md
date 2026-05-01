# Smart IT Service Desk & System Monitoring Automation

A comprehensive Python-based IT service desk automation system that handles ticket management, SLA tracking, system monitoring, and reporting.

## Features

### Ticket Management
- Auto-generated ticket IDs
- Priority-based ticket classification (P1-P4)
- Ticket status tracking (Open/In Progress/Closed)
- Search, update, and delete functionality
- ITIL-based incident and service request management

### SLA Management
- Automatic SLA deadline calculation
- SLA breach detection and alerts
- Escalation alerts for critical tickets
- Real-time SLA monitoring

### System Monitoring
- CPU, RAM, and Disk usage monitoring
- Automatic high-priority ticket generation for threshold breaches
- Configurable monitoring thresholds
- Continuous monitoring capability

### File Handling & Logging
- JSON-based ticket storage
- CSV backup functionality
- Comprehensive event logging
- Data persistence and recovery

### Reporting
- Daily summary reports
- Monthly trend analysis
- Custom date range reports
- CSV export functionality

## Project Structure

```
smart_it_service_desk/
|
|--- main.py              # Main application entry point
|--- tickets.py           # Ticket management classes
|--- monitor.py           # System monitoring module
|--- reports.py           # Report generation system
|--- utils.py             # Utility functions and helpers
|--- data/                # Data storage directory
|    |--- tickets.json    # Ticket database
|    |--- logs.txt        # System logs
|    |--- backup.csv      # Ticket backups
|--- README.md            # This file
```

## Installation

### Prerequisites
- Python 3.7 or higher
- pip package manager

### Required Packages
```bash
pip install psutil
```

### Setup
1. Clone or download the project
2. Navigate to the project directory
3. Install required packages:
   ```bash
   pip install psutil
   ```
4. Run the application:
   ```bash
   python main.py
   ```

## Usage

### Main Menu Options

1. **Create Ticket** - Raise a new support ticket
2. **View All Tickets** - Display all existing tickets
3. **Search Ticket by ID** - Find specific tickets
4. **Update Ticket Status** - Change ticket status
5. **Close Ticket** - Mark tickets as resolved
6. **Delete Ticket** - Remove tickets from system
7. **System Monitoring** - Check system health
8. **Generate Reports** - Create daily/monthly reports
9. **View Logs** - Check system activity logs
10. **Exit** - Close the application

### Priority Classification

| Issue Type | Priority | SLA Time |
|------------|----------|----------|
| Server Down | P1 | 1 Hour |
| Internet Down | P2 | 4 Hours |
| Laptop Slow | P3 | 8 Hours |
| Password Reset | P4 | 24 Hours |

### System Monitoring Thresholds

- **CPU Usage**: > 90%
- **RAM Usage**: > 95%
- **Disk Free Space**: < 10%

## ITIL Implementation

### Incident Management
- Automatic incident ticket creation
- Priority-based classification
- SLA tracking and breach detection

### Service Request Management
- Standard and special request handling
- Approval workflow for special requests
- Request categorization

### Problem Management
- Automatic problem record creation for recurring issues
- Root cause analysis tracking
- Solution documentation

### Change Request Tracking
- Change impact assessment
- Approval status tracking
- Implementation monitoring

## Debugging Features

The system includes comprehensive debugging capabilities:

### Error Handling
- File not found errors
- Invalid input validation
- Empty field validation
- Ticket ID validation

### Logging System
- Event timestamping
- Error logging
- Operation tracking
- Log file management

### Debug Utilities
- Variable inspection
- Function call tracing
- Error context logging
- Debug information display

## Reports

### Daily Summary Report
- Total tickets raised
- Open/closed ticket counts
- High priority tickets
- SLA breach statistics
- Escalation alerts

### Monthly Trend Report
- Most common issues
- Department analysis
- Average resolution times
- Weekly trends
- Problem records

## Data Storage

### Primary Storage
- **tickets.json**: Main ticket database
- **logs.txt**: System activity logs
- **backup.csv**: CSV backup of tickets

### Data Persistence
- Automatic data loading on startup
- Real-time data saving
- Backup creation on ticket updates
- Data recovery options

## System Requirements

### Minimum Requirements
- Python 3.7+
- 100MB free disk space
- 512MB RAM
- Basic system access permissions

### Recommended Requirements
- Python 3.9+
- 500MB free disk space
- 1GB RAM
- Administrative privileges for system monitoring

## Configuration

### Monitoring Thresholds
Edit thresholds in `monitor.py`:
```python
self.thresholds = {
    "cpu": 90.0,      # CPU usage threshold in %
    "ram": 95.0,      # RAM usage threshold in %
    "disk": 10.0      # Minimum free disk space in %
}
```

### SLA Settings
Modify SLA times in `tickets.py`:
```python
sla_hours = {
    "P1": 1,    # hours
    "P2": 4,    # hours
    "P3": 8,    # hours
    "P4": 24    # hours
}
```

## Troubleshooting

### Common Issues

1. **Permission Errors**: Run with appropriate permissions
2. **File Access Errors**: Check data directory permissions
3. **Import Errors**: Verify psutil installation
4. **JSON Parse Errors**: Check tickets.json file integrity

### Log Analysis
Check `data/logs.txt` for detailed error information and system events.

## Development

### Code Structure
- **OOP Design**: Class-based architecture
- **Inheritance**: Ticket hierarchy (Incident, Service Request)
- **Encapsulation**: Private methods and data protection
- **Polymorphism**: Method overriding in subclasses

### Key Classes
- `Ticket`: Base ticket class
- `IncidentTicket`: Incident management
- `ServiceRequest`: Service request handling
- `ProblemRecord`: Problem management
- `SystemMonitor`: Health monitoring
- `ReportGenerator`: Report creation
- `TicketManager`: Main ticket operations

## Security Considerations

- Input validation for all user inputs
- File access permissions
- Error message sanitization
- Log file access control
- Data backup encryption (optional)

## Future Enhancements

- Web interface development
- Database integration (SQLite/MySQL)
- Email notification system
- API integration for external systems
- Multi-user support with authentication
- Advanced analytics dashboard
- Mobile application support

## Support

For issues and questions:
1. Check the logs file for error details
2. Verify system requirements
3. Review configuration settings
4. Test with sample data

## License

This project is for educational and demonstration purposes.

---

**Note**: This is an offline system designed for internal IT support operations. No external API dependencies are required for core functionality.
