using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Anthoro.NetExensions.Extensions;

namespace Anthoro.NetExensions.Html
{
    public static class AngularHtmlHelper
    {
        /* Form Controls */

        public static MvcHtmlString NgFormTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string scopeVariable = null, string model = null,
            string labelText = null, bool disabled = false, string containerClass = null, string labelClass = null, string controlClass = null,
            Dictionary<string, object> htmlAttributes = null)
        {
            string id;
            var ngModel = GetNgModel(html, expression, scopeVariable, model, ref labelText, out id);

            // label
            var label = new TagBuilder("label");
            label.MergeAttribute("for", id);
            label.InnerHtml = labelText;
            label.AddCssClass("control-label");
            if (labelClass.IsNotNullOrWhiteSpace())
            {
                label.AddCssClass(labelClass);
            }

            // TextBox
            var @class = new List<string> { "form-control" };
            if (controlClass.IsNotNullOrWhiteSpace())
            {
                @class.Add(controlClass);
            }

            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }
            htmlAttributes.Add("class", @class.Join(" "));
            htmlAttributes.Add("ng-model", ngModel);

            if (disabled)
            {
                htmlAttributes.Add("disabled", null);
            }

            var input = html.TextBoxFor(expression, htmlAttributes);

            // container div
            var divContainer = new TagBuilder("div")
            {
                InnerHtml = string.Format("{0}{1}", label, input)
            };
            divContainer.AddCssClass("form-group");

            // div
            var div = new TagBuilder("div");
            if (containerClass.IsNotNullOrWhiteSpace())
            {
                div.AddCssClass(containerClass);
            }
            div.InnerHtml = divContainer.ToString();

            return new MvcHtmlString(div.ToString());
        }

        public static MvcHtmlString NgFormNumericBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string scopeVariable = null, string model = null,
            string labelText = null)
        {
            return html.NgFormTextBoxFor(expression, scopeVariable, model, labelText);
        }

        public static MvcHtmlString NgFormDateBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string scopeVariable = null, string model = null,
            string labelText = null, bool disabled = false,
            string containerClass = null, string labelClass = null, string controlClass = null)
        {
            return html.NgFormTextBoxFor(expression, scopeVariable, model, labelText, disabled, containerClass, labelClass, controlClass, new Dictionary<string, object> { { "type", "date" } });
        }

        public static MvcHtmlString NgFormCheckBoxFor<TModel>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, bool>> expression,
            string scopeVariable = null, string labelText = null, bool disabled = false,
            string containerClass = null)
        {
            // div
            var div = new TagBuilder("div");
            if (containerClass.IsNotNullOrWhiteSpace())
            {
                div.AddCssClass(containerClass);
            }

            div.InnerHtml = html.NgCheckBoxFor(expression, scopeVariable, labelText, disabled).ToString();

            return new MvcHtmlString(div.ToString());
        }

        public static MvcHtmlString NgFormRadioButtonFor<TModel>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, bool>> expression,
            string scopeVariable = null, string labelText = null)
        {
            var ngModel = GetNgModel(html, expression, scopeVariable, ref labelText);

            // div
            var div = new TagBuilder("div");
            div.AddCssClass("radio");

            // label
            var label = new TagBuilder("label");

            // CheckBox
            var input = html.RadioButtonFor(expression, new { ng_model = ngModel });

            label.InnerHtml = string.Format("{0}{1}", input, labelText);
            div.InnerHtml = label.ToString();

            return new MvcHtmlString(div.ToString());
        }

        public static MvcHtmlString NgFormDropDownListForFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string scopeVariable = null, string scopeListVariable = null,
            string model = null, string labelText = null)
        {
            string id;
            var ngModel = GetNgModel(html, expression, scopeVariable, model, ref labelText, out id);

            // div
            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            // label
            var label = new TagBuilder("label");
            label.MergeAttribute("for", id);
            label.InnerHtml = labelText;

            // DropDownList
            var input = new TagBuilder("select");
            input.MergeAttributes(new Dictionary<string, object>
            {
                {"class", "form-control"},
                {"ng_model", ngModel},
                {"type", "number"},
                {"ng_options", string.Format("item.Key as item.Value for item in {0}", scopeListVariable)}
            });

            div.InnerHtml = string.Format("{0}{1}", label, input);

            return new MvcHtmlString(div.ToString());
        }

        public static MvcHtmlString NgFormDisplayTextFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string scopeVariable = null, string model = null,
            string labelText = null)
        {
            string id;
            var ngModel = GetNgModel(html, expression, scopeVariable, model, ref labelText, out id);

            // div
            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            // label
            var label = new TagBuilder("label");
            label.MergeAttribute("for", id);
            label.InnerHtml = labelText;

            // p
            var p = new TagBuilder("p");
            p.MergeAttribute("class", "form-control-static");
            p.InnerHtml = string.Format("{{{{{0}}}}}", ngModel);

            div.InnerHtml = string.Format("{0}{1}", label, p);

            return new MvcHtmlString(div.ToString());
        }

        public static MvcHtmlString NgFormTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string scopeVariable = null, string model = null, bool disabled = false,
            string labelText = null, string labelClass = null, string controlClass = null, string containerClass = null,
            Dictionary<string, object> htmlAttributes = null)
        {
            string id;
            var ngModel = GetNgModel(html, expression, scopeVariable, model, ref labelText, out id);

            // div
            var div = new TagBuilder("div");
            div.AddCssClass("form-group");

            // container div 
            var divContainer = new TagBuilder("div");
            if (containerClass.IsNotNullOrWhiteSpace())
            {
                divContainer.AddCssClass(containerClass);
            }

            // label
            TagBuilder label = null;
            if (labelText != string.Empty)
            {
                label = new TagBuilder("label");
                label.MergeAttribute("for", id);
                label.InnerHtml = labelText;
                label.AddCssClass("control-label");
                if (labelClass.IsNotNullOrWhiteSpace())
                {
                    label.AddCssClass(labelClass);
                }
            }

            // TextArea
            var @class = new List<string> { "form-control" };
            if (controlClass.IsNotNullOrWhiteSpace())
            {
                @class.Add(controlClass);
            }

            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }
            htmlAttributes.Add("class", @class.Join(" "));
            htmlAttributes.Add("ng-model", ngModel);
            if (disabled)
            {
                htmlAttributes.Add("disabled", null);
            }
            var input = html.TextAreaFor(expression, htmlAttributes);

            divContainer.InnerHtml = string.Format("{0}{1}", label != null ? label.ToString() : "", input);
            div.InnerHtml = divContainer.ToString();

            return new MvcHtmlString(div.ToString());
        }

        /* HTML controls */

        public static MvcHtmlString NgCheckBoxFor<TModel>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, bool>> expression,
            string scopeVariable = null, string labelText = null, bool disabled = false)
        {
            var ngModel = GetNgModel(html, expression, scopeVariable, ref labelText);

            // CheckBox container
            var divCheckBox = new TagBuilder("div");
            divCheckBox.AddCssClass("checkbox");

            // Checkbox
            var htmlAttributes = new Dictionary<string, object> { { "ng-model", ngModel } };
            if (disabled)
            {
                htmlAttributes.Add("disabled", null);
            }
            var input = html.CheckBoxFor(expression, htmlAttributes);

            // label
            var label = new TagBuilder("label")
            {
                InnerHtml = string.Format("{0}{1}", input, labelText)
            };

            divCheckBox.InnerHtml = label.ToString();

            return new MvcHtmlString(divCheckBox.ToString());
        }


        /* IdFor */

        public static string NgIdFor<TModel, TProperty>(this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression, string scopeVariable = null)
        {
            var modelMetadata = html.ViewData.ModelMetadata;

            scopeVariable = scopeVariable ?? modelMetadata.ModelType.Name;

            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var name = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);

            return string.Format("{0}.{1}", scopeVariable, name);
        }

        /* Private methods */

        private static string GetNgModel<TModel>(HtmlHelper<TModel> html,
            Expression<Func<TModel, bool>> expression, string scopeVariable,
            ref string labelText)
        {
            var modelMetadata = html.ViewData.ModelMetadata;
            var propertyMetaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            scopeVariable = scopeVariable ?? modelMetadata.ModelType.Name;

            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var name = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText);
            labelText = labelText
                        ?? propertyMetaData.DisplayName
                        ?? propertyMetaData.PropertyName
                        ?? modelMetadata.DisplayName
                        ?? modelMetadata.PropertyName;

            return string.Format("{0}.{1}", scopeVariable, name);
        }

        private static string GetNgModel<TModel, TProperty>(HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression, string scopeVariable, string model,
            ref string labelText, out string id)
        {
            var modelMetadata = html.ViewData.ModelMetadata;
            var propertyMetaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            scopeVariable = scopeVariable ?? modelMetadata.ModelType.Name;

            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var name = model.IsNullOrWhiteSpace()
                //? html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText)
                ? html.ViewData.TemplateInfo.GetFullHtmlFieldName(expressionText)
                : model;
            labelText = labelText
                        ?? propertyMetaData.DisplayName
                        ?? propertyMetaData.PropertyName
                        ?? modelMetadata.DisplayName
                        ?? modelMetadata.PropertyName;

            id = html.IdFor(expression).ToString();

            return string.Format("{0}.{1}", scopeVariable, name);
        }
    }
}