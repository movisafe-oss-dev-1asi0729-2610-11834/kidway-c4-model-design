using Structurizr;

namespace kidway_c4_model_design
{
    public class UserProfilesComponentDiagram
    {
        private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "UserProfilesComponent";

        public Component profile_controller { get; private set; }
        public Component preferences_controller { get; private set; }
        public Component profile_service { get; private set; }
        public Component preferences_service { get; private set; }
        public Component profile_repository { get; private set; }
        public Component profile_entity { get; private set; }

        public UserProfilesComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            profile_controller = containerDiagram.rest_api.AddComponent(
                "Profile Controller",
                "Handles requests for viewing and updating KidWay user profile information.",
                "Java, Spring Boot REST Controller"
            );

            preferences_controller = containerDiagram.rest_api.AddComponent(
                "Preferences Controller",
                "Handles requests for language, notification, and account preference settings.",
                "Java, Spring Boot REST Controller"
            );

            profile_service = containerDiagram.rest_api.AddComponent(
                "Profile Service",
                "Applies profile update rules for operators, companies, and administrators.",
                "Java, Spring Service"
            );

            preferences_service = containerDiagram.rest_api.AddComponent(
                "Preferences Service",
                "Applies user preference rules for notifications, language, and interface settings.",
                "Java, Spring Service"
            );

            profile_repository = containerDiagram.rest_api.AddComponent(
                "Profile Repository",
                "Reads and writes user profile and preference data.",
                "Spring Data JPA Repository"
            );

            profile_entity = containerDiagram.rest_api.AddComponent(
                "Profile Entity",
                "Represents user profile data, contact information, preferences, and assigned role.",
                "Java Entity"
            );
        }

        private void AddRelationships()
        {
            contextDiagram.independent_operator.Uses(
                profile_controller,
                "Manages personal profile",
                "JSON/HTTPS"
            );

            contextDiagram.independent_operator.Uses(
                preferences_controller,
                "Configures profile preferences",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                profile_controller,
                "Manages company user profiles",
                "JSON/HTTPS"
            );

            contextDiagram.transport_company.Uses(
                preferences_controller,
                "Configures notification preferences",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                profile_controller,
                "Reviews and manages user profile records",
                "JSON/HTTPS"
            );

            contextDiagram.kidway_administrator.Uses(
                preferences_controller,
                "Reviews user preference settings",
                "JSON/HTTPS"
            );

            profile_controller.Uses(
                profile_service,
                "Delegates profile logic"
            );

            preferences_controller.Uses(
                preferences_service,
                "Delegates preference logic"
            );

            profile_service.Uses(
                profile_repository,
                "Persists profile data"
            );

            preferences_service.Uses(
                profile_repository,
                "Persists preference data"
            );

            profile_repository.Uses(
                profile_entity,
                "Maps data to profile model"
            );

            profile_repository.Uses(
                containerDiagram.database,
                "Reads and writes profile data",
                "SQL"
            );
        }

        private void ApplyStyles()
        {
            SetTags();

            Styles styles = c4.ViewSet.Configuration.Styles;

            styles.Add(new ElementStyle(componentTag)
            {
                Background = "#2E7D32",
                Color = "#ffffff",
                Shape = Shape.Component
            });
        }

        private void SetTags()
        {
            profile_controller.AddTags(componentTag);
            preferences_controller.AddTags(componentTag);
            profile_service.AddTags(componentTag);
            preferences_service.AddTags(componentTag);
            profile_repository.AddTags(componentTag);
            profile_entity.AddTags(componentTag);
        }

        private void CreateView()
        {
            ComponentView componentView = c4.ViewSet.CreateComponentView(
                containerDiagram.rest_api,
                "kidway-component-user-profiles",
                "Component Diagram - User Profiles Bounded Context"
            );

            componentView.Title = "KidWay - User Profiles";

            componentView.Add(contextDiagram.independent_operator);
            componentView.Add(contextDiagram.transport_company);
            componentView.Add(contextDiagram.kidway_administrator);

            componentView.Add(profile_controller);
            componentView.Add(preferences_controller);
            componentView.Add(profile_service);
            componentView.Add(preferences_service);
            componentView.Add(profile_repository);
            componentView.Add(profile_entity);

            componentView.Add(containerDiagram.database);
        }
    }
}