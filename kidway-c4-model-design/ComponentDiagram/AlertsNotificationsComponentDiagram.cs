using Structurizr;

namespace kidway_c4_model_design
{
    public class AlertsNotificationsComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "AlertsNotificationsComponent";

        public Component notification_controller { get; private set; }
        public Component alert_controller { get; private set; }
        public Component notification_service { get; private set; }
        public Component alert_service { get; private set; }
        public Component notification_repository { get; private set; }
        public Component notification_entity { get; private set; }

        public AlertsNotificationsComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
        {
            this.c4 = c4;
            this.contextDiagram = contextDiagram;
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
            notification_controller = containerDiagram.rest_api.AddComponent(
                "Notification Controller",
                "Handles requests for sending and reviewing operational notifications.",
                "Java, Spring Boot REST Controller"
            );

            alert_controller = containerDiagram.rest_api.AddComponent(
                "Alert Controller",
                "Handles requests for delay alerts, route incidents, and emergency notifications.",
                "Java, Spring Boot REST Controller"
            );

            notification_service = containerDiagram.rest_api.AddComponent(
                "Notification Service",
                "Coordinates push notifications, status messages, reminders, and communication events.",
                "Java, Spring Service"
            );

            alert_service = containerDiagram.rest_api.AddComponent(
                "Alert Service",
                "Applies alert rules for delays, incidents, route deviations, and operational risks.",
                "Java, Spring Service"
            );

            notification_repository = containerDiagram.rest_api.AddComponent(
                "Notification Repository",
                "Reads and writes notifications, alerts, delivery status, and communication history.",
                "Spring Data JPA Repository"
            );

            notification_entity = containerDiagram.rest_api.AddComponent(
                "Notification Entity",
                "Represents alerts, notifications, recipients, status, priority, and timestamps.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                notification_controller,
                "Receives operational notifications",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                alert_controller,
                "Receives alerts and fleet notifications",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                alert_controller,
                "Reviews platform alerts",
                "JSON/HTTPS"
            );

            notification_controller.Uses(
                notification_service,
                "Delegates notification logic"
            );

            alert_controller.Uses(
                alert_service,
                "Delegates alert logic"
            );

            notification_service.Uses(
                alert_service,
                "Requests alert validation"
            );

            notification_service.Uses(
                notification_repository,
                "Persists notification data"
            );

            alert_service.Uses(
                notification_repository,
                "Reads alert history"
            );

            notification_repository.Uses(
                notification_entity,
                "Maps data to notification model"
            );

            notification_repository.Uses(
                containerDiagram.database,
                "Reads and writes alerts data",
                "SQL"
            );

            notification_service.Uses(
                contextDiagram.notification_service,
                "Sends push notifications",
                "JSON/HTTPS"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#C62828",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            notification_controller.AddTags(componentTag);
            alert_controller.AddTags(componentTag);
            notification_service.AddTags(componentTag);
            alert_service.AddTags(componentTag);
            notification_repository.AddTags(componentTag);
            notification_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-alerts-notifications",
                "Component Diagram - Alerts & Notifications Bounded Context"
            );

            componentView.Title = "KidWay - Alerts & Notifications";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(notification_controller);
            componentView.Add(alert_controller);
            componentView.Add(notification_service);
            componentView.Add(alert_service);
            componentView.Add(notification_repository);
            componentView.Add(notification_entity);

            componentView.Add(containerDiagram.database);
            componentView.Add(contextDiagram.notification_service);
        }
    }
}