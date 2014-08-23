using Polling.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polling.WebUI.CustomBinding
{
    public class CreatePollBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType == typeof(PollViewModel))
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;

                Dictionary<string, string> values = new Dictionary<string, string>(request.Form.Keys.Count);
                // get each request parameter for the view model
                // and create a viewmodel here
                foreach (var item in request.Form.Keys)
                {
                    string value = request.Form.Get(item.ToString());

                    values.Add(item.ToString(), value);
                }

                PollViewModel vm = CreateViewModel(values);
                return vm;
            }
            else
            {
                return base.BindModel(controllerContext, bindingContext);
            }            
        }

        private PollViewModel CreateViewModel(Dictionary<string, string> values)
        {
            PollViewModel vm = new PollViewModel();

            vm.Poll = new Domain.Entities.Poll()
            {
                Description = values["Poll.Description"],
                PollQuestion = values["Poll.PollQuestion"],
                Meta = values["Poll.Meta"],
                UrlSlug = values["Poll.UrlSlug"],
                CategoryID = int.Parse(values["SelectedCategoryId"]),
            };

            vm.Options = values
                .Where(x => x.Key.Contains("OptionObject"))
                .Where(x => x.Key.Contains("Text"))
                .Select(x => new Domain.Entities.Option
                {
                    Text = x.Value
                });

            vm.SelectedCategoryId = int.Parse(values["SelectedCategoryId"]);

            return vm;
        }
    }
}