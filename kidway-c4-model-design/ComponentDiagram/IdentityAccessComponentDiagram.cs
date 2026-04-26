using Structurizr;

namespace kidway_c4_model_design
{
    public class IdentityAccessComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "IdentityAccessComponent";

        public Component auth_controller { get; private set; }
        public Component user_controller { get; private set; }
        public Component auth_service { get; private set; }
        public Component token_service { get; private set; }
        public Component user_repository { get; private set; }

        public IdentityAccessComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            auth_controller = containerDiagram.rest_api.AddComponent(
                "Auth Controller",
                "Handles login, logout, registration, and token validation requests.",
                "Java, Spring Boot REST Controller"
            );

            user_controller = containerDiagram.rest_api.AddComponent(
                "User Controller",
                "Handles user account, role, and permission management requests.",
                "Java, Spring Boot REST Controller"
            );

            auth_service = containerDiagram.rest_api.AddComponent(
                "Auth Service",
                "Coordinates authentication rules and validates user credentials.",
                "Java, Spring Service"
            );

            token_service = containerDiagram.rest_api.AddComponent(
                "Token Service",
                "Generates, validates, and refreshes JWT access tokens.",
                "Java, Spring Security, JWT"
            );

            user_repository = containerDiagram.rest_api.AddComponent(
                "User Repository",
                "Reads and writes user, role, and permission data.",
                "Spring Data JPA Repository"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                auth_controller,
                "Authenticates to access KidWay",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                auth_controller,
                "Authenticates to access KidWay",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                auth_controller,
                "Authenticates and manages access",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                user_controller,
                "Manages users, roles, and permissions",
                "JSON/HTTPS"
            );

            auth_controller.Uses(
                auth_service,
                "Delegates authentication logic"
            );

            user_controller.Uses(
                user_repository,
                "Manages identity data"
            );

            auth_service.Uses(
                user_repository,
                "Validates credentials and retrieves user roles"
            );

            auth_service.Uses(
                token_service,
                "Generates and validates JWT tokens"
            );

            user_repository.Uses(
                containerDiagram.database,
                "Reads and writes identity data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#6A1B9A",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            auth_controller.AddTags(componentTag);
            user_controller.AddTags(componentTag);
            auth_service.AddTags(componentTag);
            token_service.AddTags(componentTag);
            user_repository.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-identity-access",
                "Component Diagram - Identity & Access Management Bounded Context"
            );

            componentView.Title = "KidWay - Identity & Access Management";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(auth_controller);
            componentView.Add(user_controller);
            componentView.Add(auth_service);
            componentView.Add(token_service);
            componentView.Add(user_repository);

            componentView.Add(containerDiagram.database);
        }
    }
}