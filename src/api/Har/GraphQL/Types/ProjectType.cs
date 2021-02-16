using GraphQL.Types;
using Har.Domain.Components;
using Har.Domain.Models;

namespace Har.GraphQL.Types
{
    public sealed class ProjectType : ObjectGraphType<Project>
    {
        public ProjectType()
        {
            Field(p => p.Id);
            Field(p => p.Name);
            Field(p => p.Author);
            Field(p => p.Technologies);
            Field(p => p.Url);
            Field(p => p.OnlineSince);
            Field(p => p.ShortDescription);
            Field(p => p.Content);
            
            Field<ComponentsType>("components", 
                resolve: context =>
                {
                    return new IComponent[]
                    {
                        new TextComponent
                        {
                            Content = "Text component 1",
                        },
                        // new ImagesComponent
                        // {
                        //     Images = new[]
                        //     {
                        //         new Image
                        //         {
                        //             ImageUrl = "https://www.elunduscore.com/images/monkaw_sm.png"
                        //         },
                        //         new Image
                        //         {
                        //             ImageUrl = "https://www.elunduscore.com/images/monkaw_sm.png"
                        //         }
                        //     }
                        // },
                        new TextComponent
                        {
                            Content = "Text component 2",
                        },
                    };
                    // return componentParser.Parse(context.Source.Content);
                });
        }
    }
}