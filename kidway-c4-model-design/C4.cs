using Structurizr;
using Structurizr.Api;

namespace kidway_c4_model_design
{
    public class C4
    {
        // Structurizr configuration
        private readonly long workspaceId = 109642;
        private readonly string apiKey = "1ca22d02-be5c-49b9-a2ea-66b92d84bacf";
        private readonly string apiSecret = "cc4313c5-3af0-491e-bbec-634f3b029e84";

        // C4 Model
        public StructurizrClient StructurizrClient { get; }
        public Workspace Workspace { get; }
        public Model Model { get; }
        public ViewSet ViewSet { get; }

        // Constructor
        public C4()
        {
            // Initialize Structurizr client and workspace
            string workspaceName = "KidWay - DDD";
            string workspaceDescription = "Domain-Driven Software Architecture for KidWay application.";

            StructurizrClient = new StructurizrClient(apiKey, apiSecret);
            Workspace = new Workspace(workspaceName, workspaceDescription);
            Model = Workspace.Model;
            ViewSet = Workspace.Views;
        }

        // Generate Method
        public void Generate()
        {
            // Create diagrams
            ContextDiagram contextDiagram =
                new ContextDiagram(this);
            ContainerDiagram containerDiagram =
                    new ContainerDiagram(this, contextDiagram);
            WebApplicationComponentDiagram webApplicationComponentDiagram =
                    new WebApplicationComponentDiagram(this, contextDiagram, containerDiagram);
            IdentityAccessComponentDiagram identityAccessComponentDiagram =
                new IdentityAccessComponentDiagram(this, contextDiagram, containerDiagram);
            UserProfilesComponentDiagram userProfilesComponentDiagram =
                new UserProfilesComponentDiagram(this, contextDiagram, containerDiagram);
            SubscriptionPaymentsComponentDiagram subscriptionPaymentsComponentDiagram =
                new SubscriptionPaymentsComponentDiagram(this, contextDiagram, containerDiagram);
            DashboardComponentDiagram dashboardComponentDiagram =
                new DashboardComponentDiagram(this, contextDiagram, containerDiagram);
            FleetManagementComponentDiagram fleetManagementComponentDiagram =
                new FleetManagementComponentDiagram(this, contextDiagram, containerDiagram);
            DriverManagementComponentDiagram driverManagementComponentDiagram =
                new DriverManagementComponentDiagram(this, contextDiagram, containerDiagram);
            RouteManagementComponentDiagram routeManagementComponentDiagram =
                new RouteManagementComponentDiagram(this, contextDiagram, containerDiagram);
            StudentManagementComponentDiagram studentManagementComponentDiagram =
                new StudentManagementComponentDiagram(this, contextDiagram, containerDiagram);
            AssignmentManagementComponentDiagram assignmentManagementComponentDiagram =
                new AssignmentManagementComponentDiagram(this, contextDiagram, containerDiagram);
            RealTimeTrackingComponentDiagram realTimeTrackingComponentDiagram =
                new RealTimeTrackingComponentDiagram(this, contextDiagram, containerDiagram);
            TripManagementComponentDiagram tripManagementComponentDiagram =
                new TripManagementComponentDiagram(this, contextDiagram, containerDiagram);
            AttendanceTrackingComponentDiagram attendanceTrackingComponentDiagram =
                new AttendanceTrackingComponentDiagram(this, contextDiagram, containerDiagram);
            
            // Generate diagrams
            contextDiagram.Generate();
            containerDiagram.Generate();
            webApplicationComponentDiagram.Generate();
            identityAccessComponentDiagram.Generate();
            userProfilesComponentDiagram.Generate();
            subscriptionPaymentsComponentDiagram.Generate();
            dashboardComponentDiagram.Generate();
            fleetManagementComponentDiagram.Generate();
            driverManagementComponentDiagram.Generate();
            routeManagementComponentDiagram.Generate();
            studentManagementComponentDiagram.Generate();
            assignmentManagementComponentDiagram.Generate();
            realTimeTrackingComponentDiagram.Generate();
            tripManagementComponentDiagram.Generate();
            attendanceTrackingComponentDiagram.Generate();
            
            // Upload workspace to Structurizr
            StructurizrClient.PutWorkspace(workspaceId, Workspace);
        }
    }
}