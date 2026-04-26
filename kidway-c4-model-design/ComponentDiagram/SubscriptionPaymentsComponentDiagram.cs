using Structurizr;

namespace kidway_c4_model_design
{
    public class SubscriptionPaymentsComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "SubscriptionPaymentsComponent";

        public Component plan_controller { get; private set; }
        public Component subscription_controller { get; private set; }
        public Component subscription_service { get; private set; }
        public Component billing_service { get; private set; }
        public Component payment_gateway_adapter { get; private set; }
        public Component billing_repository { get; private set; }

        public SubscriptionPaymentsComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            plan_controller = containerDiagram.rest_api.AddComponent(
                "Plan Controller",
                "Handles requests for available KidWay plans and plan details.",
                "Java, Spring Boot REST Controller"
            );

            subscription_controller = containerDiagram.rest_api.AddComponent(
                "Subscription Controller",
                "Handles subscription activation, renewal, cancellation, and status requests.",
                "Java, Spring Boot REST Controller"
            );

            subscription_service = containerDiagram.rest_api.AddComponent(
                "Subscription Service",
                "Applies subscription rules and manages customer access to KidWay plans.",
                "Java, Spring Service"
            );

            billing_service = containerDiagram.rest_api.AddComponent(
                "Billing Service",
                "Coordinates billing operations, invoices, and payment validation.",
                "Java, Spring Service"
            );

            payment_gateway_adapter = containerDiagram.rest_api.AddComponent(
                "Payment Gateway Adapter",
                "Integrates KidWay billing operations with the external payment gateway.",
                "Java Adapter"
            );

            billing_repository = containerDiagram.rest_api.AddComponent(
                "Billing Repository",
                "Reads and writes plans, subscriptions, invoices, and payment records.",
                "Spring Data JPA Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                plan_controller,
                "Reviews available plans",
                "JSON/HTTPS"
            );

            contextDiagram.independent_operator.Uses(
                subscription_controller,
                "Manages personal subscription",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                plan_controller,
                "Reviews company plans",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                subscription_controller,
                "Manages company subscription",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                plan_controller,
                "Manages available plans",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                subscription_controller,
                "Reviews customer subscriptions",
                "JSON/HTTPS"
            );

            plan_controller.Uses(
                subscription_service,
                "Retrieves plan information"
            );

            subscription_controller.Uses(
                subscription_service,
                "Delegates subscription logic"
            );

            subscription_service.Uses(
                billing_service,
                "Requests billing validation"
            );

            subscription_service.Uses(
                billing_repository,
                "Persists subscription data"
            );

            billing_service.Uses(
                payment_gateway_adapter,
                "Processes online payments"
            );

            billing_service.Uses(
                billing_repository,
                "Persists billing records"
            );

            payment_gateway_adapter.Uses(
                contextDiagram.payment_gateway,
                "Sends payment requests",
                "JSON/HTTPS"
            );

            billing_repository.Uses(
                containerDiagram.database,
                "Reads and writes billing data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#0277BD",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            plan_controller.AddTags(componentTag);
            subscription_controller.AddTags(componentTag);
            subscription_service.AddTags(componentTag);
            billing_service.AddTags(componentTag);
            payment_gateway_adapter.AddTags(componentTag);
            billing_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-subscription-payments",
                "Component Diagram - Subscription & Payments Bounded Context"
            );

            componentView.Title = "KidWay - Subscription & Payments";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(plan_controller);
            componentView.Add(subscription_controller);
            componentView.Add(subscription_service);
            componentView.Add(billing_service);
            componentView.Add(payment_gateway_adapter);
            componentView.Add(billing_repository);

            componentView.Add(contextDiagram.payment_gateway);
            componentView.Add(containerDiagram.database);
        }
    }
}