using Structurizr;

namespace kidway_c4_model_design
{
    public class ContextDiagram
    {
        private readonly C4 c4;

        // People
        public Person independent_operator { get; private set; }
        public Person transport_company { get; private set; }
        public Person kidway_administrator { get; private set; }

        public Person visitor { get; private set; }

        // Software Systems
        public SoftwareSystem kidway { get; private set; }
        public SoftwareSystem gps_tracking { get; private set; }
        public SoftwareSystem payment_gateway { get; private set; }
        public SoftwareSystem notification_service { get; private set; }


        // Constructor
        public ContextDiagram(C4 c4)
        {
            this.c4 = c4;
        }

        // Generate Method
        public void Generate()
        {
            AddElements();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        // Add Elements
        private void AddElements()
        {
            AddPeople();
            AddSoftwareSystems();
        }

        // Add People
        private void AddPeople()
        {
            independent_operator = c4.Model.AddPerson(
                "Independent Operator",
                "Manages an individual school transport service, using the platform to register routes, students, and monitor trips."
            );

            transport_company = c4.Model.AddPerson(
                "Transport Company",
                "Manages a fleet of school transport vehicles, using the platform to oversee drivers, routes, and operations in real time."
            );

            kidway_administrator = c4.Model.AddPerson(
                "KidWay Administrator",
                "Configures and monitors the platform, managing users and ensuring system availability and performance."
            );

            visitor = c4.Model.AddPerson(
                "Visitor",
                "Explores the public website to learn about the platform and its features."
            );
        }

        // Add Software Systems
        private void AddSoftwareSystems()
        {
            kidway = c4.Model.AddSoftwareSystem(
                "KidWay",
                "Platform that enables real-time monitoring and management of school transport, connecting operators, companies, and families to ensure safe and efficient student mobility."
            );

            gps_tracking = c4.Model.AddSoftwareSystem(
                "GPS Tracking",
                "Provides real-time vehicle location data using GPS devices."
            );

            payment_gateway = c4.Model.AddSoftwareSystem(
                "Payment Gateway",
                "Processes secure online payments for transport services."
            );

            notification_service = c4.Model.AddSoftwareSystem(
                "Notification Service",
                "Service that allows sending push notifications to users (parents, operators, companies)."
            );
        }

        // Add Relationships
        private void AddRelationships()
        {
            independent_operator.Uses(
                kidway,
                "Uses the platform to manage routes and students"
            );

            transport_company.Uses(
                kidway,
                "Uses the platform to manage fleet and operations"
            );

            kidway_administrator.Uses(
                kidway,
                "Administers and configures the platform"
            );

            visitor.Uses(
                kidway,
                "Visits the website to explore features"
            );

            // Software Systems to Software Systems
            kidway.Uses(
                gps_tracking,
                "Receives real-time location data via API"
            );

            kidway.Uses(
                payment_gateway,
                "Processes payments via external payment API"
            );

            kidway.Uses(
                notification_service,
                "Sends push notifications via messaging service"
            );
        }

        // Apply Styles
        private void ApplyStyles()
        {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;

            // People
            styles.Add(new ElementStyle(nameof(independent_operator))
            {
                Background = "#8e24aa",
                Color = "#ffffff",
                Shape = Shape.Person
            });

            styles.Add(new ElementStyle(nameof(transport_company))
            {
                Background = "#f9a825",
                Color = "#ffffff",
                Shape = Shape.Person
            });

            styles.Add(new ElementStyle(nameof(kidway_administrator))
            {
                Background = "#4D1D6E",
                Color = "#ffffff",
                Shape = Shape.Person
            });

            styles.Add(new ElementStyle(nameof(visitor))
            {
                Background = "#85324C",
                Color = "#ffffff",
                Shape = Shape.Person
            });

            // Software Systems
            styles.Add(new ElementStyle(nameof(kidway))
            {
                Background = "#2e7d32",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });

            styles.Add(new ElementStyle(nameof(gps_tracking))
            {
                Background = "#6d4c41",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });

            styles.Add(new ElementStyle(nameof(payment_gateway))
            {
                Background = "#19ACFA",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });

            styles.Add(new ElementStyle(nameof(notification_service))
            {
                Background = "#631818",
                Color = "#ffffff",
                Shape = Shape.RoundedBox
            });
        }

        // Set Tags
        private void SetTags()
        {
            // People
            independent_operator.AddTags(nameof(independent_operator));
            transport_company.AddTags(nameof(transport_company));
            kidway_administrator.AddTags(nameof(kidway_administrator));
            visitor.AddTags(nameof(visitor));

            // Software Systems
            kidway.AddTags(nameof(kidway));
            gps_tracking.AddTags(nameof(gps_tracking));
            payment_gateway.AddTags(nameof(payment_gateway));
            notification_service.AddTags(nameof(notification_service));
        }

        // Create View
        private void CreateView()
        {
            SystemContextView contextView = c4.ViewSet.CreateSystemContextView(
                kidway,
                "kidway-context",
                "Context Diagram - KidWay System Context"
            );

            // Add all people and software systems to the view
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();
        }
    }
}