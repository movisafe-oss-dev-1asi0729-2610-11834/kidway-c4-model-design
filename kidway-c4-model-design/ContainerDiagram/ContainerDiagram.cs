using Structurizr;

namespace kidway_c4_model_design
{
    public class ContainerDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;

        public Container mobile_application { get; private set; }
        public Container business_website { get; private set; }
        public Container web_application { get; private set; }
        public Container rest_api { get; private set; }
        public Container database { get; private set; }

        public ContainerDiagram(C4 c4, ContextDiagram contextDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
        }

        public void Generate()
        {
            AddContainers();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        private void AddContainers()
        {
            mobile_application = contextDiagram.kidway.AddContainer(
                "Mobile App",
                "Mobile interface used by operators to manage assigned routes, students, trip status, and real-time transport updates.",
                "Flutter"
            );

            business_website = contextDiagram.kidway.AddContainer(
                "Business Website",
                "Public business website that presents KidWay, its features, benefits, pricing plans, and contact options for potential customers.",
                "HTML5, CSS3, JavaScript"
            );

            web_application = contextDiagram.kidway.AddContainer(
                "Web App",
                "Responsive web application used by transport companies and administrators to manage routes, vehicles, users, payments, and operational monitoring.",
                "Angular, Angular Material, TypeScript"
            );

            rest_api = contextDiagram.kidway.AddContainer(
                "REST API",
                "Backend service that exposes secure RESTful endpoints, handles business rules, integrates external services, and manages data access.",
                "Java, Spring Boot, OpenAPI/Swagger"
            );

            database = contextDiagram.kidway.AddContainer(
                "Database",
                "Stores operational data such as users, students, vehicles, routes, trips, payments, subscriptions, and notifications.",
                "SQL Server"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(mobile_application, "Manages routes, students, and trips");
            contextDiagram.independent_operator.Uses(business_website, "Explores service information");
            contextDiagram.independent_operator.Uses(web_application, "Manages transport operations");

            contextDiagram.transport_company.Uses(mobile_application, "Monitors assigned transport activities");
            contextDiagram.transport_company.Uses(business_website, "Reviews plans and service benefits");
            contextDiagram.transport_company.Uses(web_application, "Manages fleet, drivers, routes, and operations");

            contextDiagram.kidway_administrator.Uses(mobile_application, "Reviews operational status");
            contextDiagram.kidway_administrator.Uses(business_website, "Reviews public business content");
            contextDiagram.kidway_administrator.Uses(web_application, "Configures users, plans, and platform settings");

            contextDiagram.visitor.Uses(business_website, "Browses public information and features");

            business_website.Uses(web_application, "Redirects users to interactive features", "HTTPS");
            business_website.Uses(mobile_application, "Provides access links to the mobile app", "HTTPS");

            mobile_application.Uses(rest_api, "Consumes transport and tracking services", "JSON/HTTPS");
            web_application.Uses(rest_api, "Consumes management and monitoring services", "JSON/HTTPS");

            rest_api.Uses(database, "Reads and writes system data", "SQL");
            rest_api.Uses(contextDiagram.gps_tracking, "Requests geolocation and tracking data", "JSON/HTTPS");
            rest_api.Uses(contextDiagram.payment_gateway, "Processes payments and subscriptions", "JSON/HTTPS");
            rest_api.Uses(contextDiagram.notification_service, "Sends alerts and notifications", "JSON/HTTPS");
        }

        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(nameof(mobile_application)) { Background = "#009688", Color = "#ffffff", Shape = Shape.MobileDevicePortrait });
            styles.Add(new ElementStyle(nameof(business_website)) { Background = "#ef6c00", Color = "#ffffff", Shape = Shape.Folder });
            styles.Add(new ElementStyle(nameof(web_application)) { Background = "#1565c0", Color = "#ffffff", Shape = Shape.WebBrowser });
            styles.Add(new ElementStyle(nameof(rest_api)) { Background = "#455a64", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle(nameof(database)) { Background = "#303f9f", Color = "#ffffff", Shape = Shape.Cylinder });
        }

        private void SetTags()
        {
            mobile_application.AddTags(nameof(mobile_application));
            business_website.AddTags(nameof(business_website));
            web_application.AddTags(nameof(web_application));
            rest_api.AddTags(nameof(rest_api));
            database.AddTags(nameof(database));
        }

        private void CreateView()
        {
            ContainerView containerView = c4.ViewSet.CreateContainerView(
                contextDiagram.kidway,
                "kidway-container",
                "Container Diagram - KidWay System"
            );

            containerView.AddAllElements();
        }
    }
}
