using Structurizr;

namespace kidway_c4_model_design
{
    public class WebApplicationComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "WebApplicationComponent";

        public Component iam_component { get; private set; }
        public Component user_profiles_component { get; private set; }
        public Component subscription_payments_component { get; private set; }
        public Component dashboard_component { get; private set; }

        public Component fleet_management_component { get; private set; }
        public Component driver_management_component { get; private set; }
        public Component route_management_component { get; private set; }
        public Component student_management_component { get; private set; }
        public Component assignment_management_component { get; private set; }

        public Component real_time_tracking_component { get; private set; }
        public Component trip_management_component { get; private set; }
        public Component attendance_tracking_component { get; private set; }
        public Component alerts_notifications_component { get; private set; }
        public Component incident_management_component { get; private set; }

        public Component analytics_reports_component { get; private set; }
        public Component company_management_component { get; private set; }

        public WebApplicationComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
        {
            this.c4 = c4;
            this.containerDiagram = containerDiagram;
        }

        public void Generate()
        {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        private void AddComponents()
        {
            iam_component = containerDiagram.web_application.AddComponent(
                "Identity & Access Management Component",
                "Manages authentication, authorization, roles, and secure access.",
                "Angular, TypeScript, Angular Material"
            );

            user_profiles_component = containerDiagram.web_application.AddComponent(
                "User Profiles Component",
                "Manages user personal data, account settings, and preferences.",
                "Angular, TypeScript, Angular Material"
            );

            subscription_payments_component = containerDiagram.web_application.AddComponent(
                "Subscription & Payments Component",
                "Manages plans, subscriptions, billing, and payment information.",
                "Angular, TypeScript, Angular Material"
            );

            dashboard_component = containerDiagram.web_application.AddComponent(
                "Dashboard Component",
                "Displays role-based summaries, metrics, alerts, and operational status.",
                "Angular, TypeScript, Angular Material"
            );

            fleet_management_component = containerDiagram.web_application.AddComponent(
                "Fleet Management Component",
                "Manages school transport vehicles, capacity, availability, and status.",
                "Angular, TypeScript, Angular Material"
            );

            driver_management_component = containerDiagram.web_application.AddComponent(
                "Driver Management Component",
                "Manages driver records, assignments, availability, and operational status.",
                "Angular, TypeScript, Angular Material"
            );

            route_management_component = containerDiagram.web_application.AddComponent(
                "Route Management Component",
                "Manages school transport routes, stops, schedules, and route details.",
                "Angular, TypeScript, Angular Material"
            );

            student_management_component = containerDiagram.web_application.AddComponent(
                "Student Management Component",
                "Manages students, guardians, pickup points, and school transport details.",
                "Angular, TypeScript, Angular Material"
            );

            assignment_management_component = containerDiagram.web_application.AddComponent(
                "Assignment Management Component",
                "Assigns students to routes, vehicles, and drivers.",
                "Angular, TypeScript, Angular Material"
            );

            real_time_tracking_component = containerDiagram.web_application.AddComponent(
                "Real-Time Tracking Component",
                "Displays live vehicle location, route progress, and estimated arrival time.",
                "Angular, TypeScript, Angular Material"
            );

            trip_management_component = containerDiagram.web_application.AddComponent(
                "Trip Management Component",
                "Manages trip start, progress, completion, and execution status.",
                "Angular, TypeScript, Angular Material"
            );

            attendance_tracking_component = containerDiagram.web_application.AddComponent(
                "Attendance Tracking Component",
                "Tracks student boarding and drop-off records during trips.",
                "Angular, TypeScript, Angular Material"
            );

            alerts_notifications_component = containerDiagram.web_application.AddComponent(
                "Alerts & Notifications Component",
                "Displays operational alerts, route updates, delays, and notifications.",
                "Angular, TypeScript, Angular Material"
            );

            incident_management_component = containerDiagram.web_application.AddComponent(
                "Incident Management Component",
                "Reports, reviews, and manages transport incidents.",
                "Angular, TypeScript, Angular Material"
            );

            analytics_reports_component = containerDiagram.web_application.AddComponent(
                "Analytics & Reports Component",
                "Displays reports and metrics for trips, routes, fleet, attendance, and incidents.",
                "Angular, TypeScript, Angular Material"
            );

            company_management_component = containerDiagram.web_application.AddComponent(
                "Company Management Component",
                "Manages company profile, internal users, structure, and settings.",
                "Angular, TypeScript, Angular Material"
            );
        }

        private void AddRelationships()
        {
            iam_component.Uses(
                containerDiagram.rest_api,
                "Authenticates users through IAM BC",
                "JSON/HTTPS"
            );

            user_profiles_component.Uses(
                containerDiagram.rest_api,
                "Manages user profiles through User Profiles BC",
                "JSON/HTTPS"
            );

            subscription_payments_component.Uses(
                containerDiagram.rest_api,
                "Manages plans and payments through Subscription & Payments BC",
                "JSON/HTTPS"
            );

            dashboard_component.Uses(
                containerDiagram.rest_api,
                "Loads role-based dashboard data through Dashboard BC",
                "JSON/HTTPS"
            );

            fleet_management_component.Uses(
                containerDiagram.rest_api,
                "Manages vehicles through Fleet Management BC",
                "JSON/HTTPS"
            );

            driver_management_component.Uses(
                containerDiagram.rest_api,
                "Manages drivers through Driver Management BC",
                "JSON/HTTPS"
            );

            route_management_component.Uses(
                containerDiagram.rest_api,
                "Manages routes through Route Management BC",
                "JSON/HTTPS"
            );

            student_management_component.Uses(
                containerDiagram.rest_api,
                "Manages students through Student Management BC",
                "JSON/HTTPS"
            );

            assignment_management_component.Uses(
                containerDiagram.rest_api,
                "Manages route assignments through Assignment Management BC",
                "JSON/HTTPS"
            );

            real_time_tracking_component.Uses(
                containerDiagram.rest_api,
                "Loads live tracking data through Real-Time Tracking BC",
                "JSON/HTTPS"
            );

            trip_management_component.Uses(
                containerDiagram.rest_api,
                "Manages trips through Trip Management BC",
                "JSON/HTTPS"
            );

            attendance_tracking_component.Uses(
                containerDiagram.rest_api,
                "Tracks attendance through Attendance Tracking BC",
                "JSON/HTTPS"
            );

            alerts_notifications_component.Uses(
                containerDiagram.rest_api,
                "Loads alerts through Alerts & Notifications BC",
                "JSON/HTTPS"
            );

            incident_management_component.Uses(
                containerDiagram.rest_api,
                "Manages incidents through Incident Management BC",
                "JSON/HTTPS"
            );

            analytics_reports_component.Uses(
                containerDiagram.rest_api,
                "Loads reports through Analytics & Reports BC",
                "JSON/HTTPS"
            );

            company_management_component.Uses(
                containerDiagram.rest_api,
                "Manages company settings through Company Management BC",
                "JSON/HTTPS"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#0C386A",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            iam_component.AddTags(componentTag);
            user_profiles_component.AddTags(componentTag);
            subscription_payments_component.AddTags(componentTag);
            dashboard_component.AddTags(componentTag);

            fleet_management_component.AddTags(componentTag);
            driver_management_component.AddTags(componentTag);
            route_management_component.AddTags(componentTag);
            student_management_component.AddTags(componentTag);
            assignment_management_component.AddTags(componentTag);

            real_time_tracking_component.AddTags(componentTag);
            trip_management_component.AddTags(componentTag);
            attendance_tracking_component.AddTags(componentTag);
            alerts_notifications_component.AddTags(componentTag);
            incident_management_component.AddTags(componentTag);

            analytics_reports_component.AddTags(componentTag);
            company_management_component.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.web_application,
                "kidway-component-web-application",
                "Component Diagram - Web Application interacting with KidWay bounded contexts through the REST API"
            );

            componentView.Title = "KidWay - Web Application";

            componentView.Add(iam_component);
            componentView.Add(user_profiles_component);
            componentView.Add(subscription_payments_component);
            componentView.Add(dashboard_component);

            componentView.Add(fleet_management_component);
            componentView.Add(driver_management_component);
            componentView.Add(route_management_component);
            componentView.Add(student_management_component);
            componentView.Add(assignment_management_component);

            componentView.Add(real_time_tracking_component);
            componentView.Add(trip_management_component);
            componentView.Add(attendance_tracking_component);
            componentView.Add(alerts_notifications_component);
            componentView.Add(incident_management_component);

            componentView.Add(analytics_reports_component);
            componentView.Add(company_management_component);

            componentView.Add(containerDiagram.rest_api);
        }
    }
}