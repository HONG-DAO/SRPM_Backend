# SRPM - Scientific Research Project Management System

## 📋 Overview

**SRPM** (Scientific Research Project Management) is a comprehensive web-based platform designed to modernize and streamline research project management at educational institutions. The system replaces traditional Gmail and Google Forms coordination with an AI-powered platform that automates initial evaluation of research proposals and provides structured project management capabilities.

## 🎯 Key Features

### Multi-Role Management System
- **Researcher Members**: Progress tracking and task collaboration
- **Principal Investigators**: Project oversight and team management
- **Host Institution**: Research topic creation and PI approval
- **Appraisal Council**: Project and milestone evaluations
- **Staff**: Proposal review and system management
- **Administrators**: System monitoring and user management

### Core Functionalities
- 🔐 **Google OAuth Integration** - Seamless authentication with institutional accounts
- 📊 **Real-time Project Monitoring** - Live updates and notifications
- 💰 **Funding Request Management** - Streamlined financial approval process
- 📈 **Analytics & Reporting** - Comprehensive project performance insights
- 🔄 **Task Management** - Milestone tracking and progress updates
- 🏛️ **Institutional Oversight** - Research topic creation and approval workflows

## 🏗️ System Architecture

### Backend Structure
```
SRPM_copy/
├── API/
│   ├── Controllers/          # API endpoint controllers
│   ├── Models/              # Data models and DTOs
│   └── Services/            # Business logic services
├── Data/
│   ├── Entities/           # Database entities
│   ├── Repositories/       # Data access layer
│   Migrations/         # Database migrations
└── wwwroot/               # Static files and assets
```

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 or later
- SQL Server or compatible database
- Google OAuth credentials
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/HONG-DAO/SRPM_Backend
   cd SRPM_Backend
   ```

2. **Configure Database Connection**
   Update `appsettings.json` with your database connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5437;Database=SRPM_DB; Username=SRPM; Password=password;"
     }
   }
   ```

3. **Set up Google OAuth**
   Configure Google OAuth settings in `appsettings.json`:
   ```json
   {
     "Google": {
       "ClientId": "your-google-client-id",
       "ClientSecret": "your-google-client-secret",
       "RedirectURi": "...",
       "SigninRedirectUri": "...",
       "SignupRedirectUri": "..."
     }
   }
   ```

4. **Install Dependencies**
   ```bash
   dotnet restore
   ```

5. **Run Database Migrations**
   ```bash
   dotnet ef database update
   ```

6. **Launch the Application**
   ```bash
   dotnet run
   ```

## 👥 User Roles & Permissions

### Researcher Member
- ✅ Project task updates and progress tracking
- ✅ Document uploads and milestone management
- ✅ Real-time notifications
- ✅ Funding request submissions
- ❌ Project creation or evaluation

### Principal Investigator
- ✅ All Researcher Member permissions
- ✅ Project proposal submissions
- ✅ Team management and task assignments
- ✅ Progress monitoring and analytics
- ✅ Milestone evaluations

### Host Institution
- ✅ Research topic creation and publishing
- ✅ Principal Investigator approval
- ✅ Project outcome oversight
- ❌ Day-to-day project management

### Appraisal Council
- ✅ Project evaluations and reviews
- ✅ Milestone assessments
- ✅ Quality assurance oversight
- ❌ Project modifications

### Staff
- ✅ User account management
- ✅ Transaction approval and monitoring
- ✅ Preliminary proposal evaluation
- ✅ System report generation

### Administrator
- ✅ Full system access
- ✅ System configuration management
- ✅ User role assignments
- ✅ Comprehensive reporting and analytics

## 🔧 Configuration

### Email Domain Restriction
The system restricts registration to institutional email addresses ending with `@fe.edu.vn`. This can be configured in the authentication settings.

### Project Visibility Settings
- **Public Information**: Project title, description, status, and owner
- **Private Information**: Evaluations, progress details, funding, and internal discussions
- **Access Control**: Only project members can view complete project details

## 📊 Key Features Detail

### Project Management
- Structured proposal submission workflow
- Real-time progress tracking
- Milestone-based evaluation system
- Document management and version control

### Financial Management
- Funding request submissions
- Budget breakdown tracking
- Approval workflow management
- Financial reporting and analytics

### Evaluation System
- Multi-stage evaluation process
- Peer review capabilities
- Milestone assessment tools
- Quality assurance metrics

### Notification System
- Real-time updates for all stakeholders
- Customizable notification preferences
- Email and in-app notification support
- Event-driven notification triggers

## 🔒 Security Features

- **Google OAuth Integration**: Secure authentication with institutional accounts
- **Role-Based Access Control**: Granular permissions based on user roles
- **Data Privacy**: Project information visibility controls
- **Audit Trail**: Complete transaction and activity logging

