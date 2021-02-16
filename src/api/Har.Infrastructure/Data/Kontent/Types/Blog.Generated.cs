// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System;
using System.Collections.Generic;
using Kentico.Kontent.Delivery.Abstractions;

namespace Har.Infrastructure.Data.KontentTypes
{
    public partial class Blog
    {
        public const string Codename = "blog";
        public const string BlogTypeCodename = "blog_type";
        public const string ImageCodename = "image";
        public const string PublishDateCodename = "publish_date";
        public const string TitleCodename = "title";
        public const string UrlCodename = "url";

        public IEnumerable<ITaxonomyTerm> BlogType { get; set; }
        public IEnumerable<IAsset> Image { get; set; }
        public DateTime? PublishDate { get; set; }
        public IContentItemSystemAttributes System { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
}