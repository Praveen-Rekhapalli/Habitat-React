using Sitecore.ExperienceEditor.Speak.Server.Contexts;
using Sitecore.ExperienceEditor.Speak.Server.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ExperienceEditor.Speak.Server.Responses;
using Sitecore.Shell.Applications.ContentEditor;
using Sitecore.Data;
using Sitecore.Text;

namespace Sitecore.Feature.Metadata.Infrastructure.Pipelines.PipelineProcessorRequest
{
    public class GenerateFieldEditorUrl : PipelineProcessorRequest<ItemContext>
    {
        public override PipelineProcessorResponseValue ProcessRequest()
        {
            return new PipelineProcessorResponseValue
            {
                Value = GenerateUrl()
            };
        }

        private string GenerateUrl()
        {
            var fieldList = CreateFieldDescriptors(RequestContext.Argument);
            var fieldeditorOptions = new FieldEditorOptions(fieldList);
            fieldeditorOptions.SaveItem = true;
            return fieldeditorOptions.ToUrlString().ToString();

        }

        private List<FieldDescriptor> CreateFieldDescriptors(string fields)
        {
            var fieldList = new List<FieldDescriptor>();
            var fieldString = new ListString(fields);
            foreach(string field in fieldString)
            {
                fieldList.Add(new FieldDescriptor(RequestContext.Item, field));
            }
            return fieldList;
        }
    }
    
}