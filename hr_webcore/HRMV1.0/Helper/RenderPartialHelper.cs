﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HRM.Helper
{
    public class RenderPartialHelper
    {
        public static string RenderPartialToString(ControllerContext context, string viewName, object model)
        {
            var controller = context.Controller;

            var partialView = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

            var stringBuilder = new StringBuilder();
            using (var stringWriter = new StringWriter(stringBuilder))
            {
                using (var htmlWriter = new HtmlTextWriter(stringWriter))
                {
                    controller.ViewData.Model = model;
                    partialView.View.Render(
                        new ViewContext(
                            controller.ControllerContext,
                            partialView.View,
                            controller.ViewData,
                            new TempDataDictionary(),
                            htmlWriter),
                        htmlWriter);
                }
            }
            return stringBuilder.ToString();
        }
    }
}