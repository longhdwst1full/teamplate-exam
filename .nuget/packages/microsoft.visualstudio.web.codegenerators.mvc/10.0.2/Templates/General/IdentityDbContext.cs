
namespace Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General
{
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System;
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class IdentityDbContext : IdentityDbContextBase
    {
        public virtual string TransformText()
        {
            this.Write("using Microsoft.AspNetCore.Identity.EntityFrameworkCore;\r\nusing Microsoft.EntityF" +
                    "rameworkCore;\r\nusing ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.UserClassModel.UserClassNamespace));
            this.Write(";\r\n\r\nnamespace ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.DbContextNamespace));
            this.Write("\r\n{\r\n    public class ");
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.DbContextName));
            this.Write("(DbContextOptions<");
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.DbContextName));
            this.Write("> options) : IdentityDbContext<");
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.UserClassModel.UserClassName));
            this.Write(">(options)\r\n    {\r\n    }\r\n}\r\n");
            return this.GenerationEnvironment.ToString();
        }
        private global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost hostValue;
        public virtual global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost Host
        {
            get
            {
                return this.hostValue;
            }
            set
            {
                this.hostValue = value;
            }
        }

private global::Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General.IdentityDbContextModel _ModelField;
private global::Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General.IdentityDbContextModel Model
{
    get
    {
        return this._ModelField;
    }
}
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool ModelValueAcquired = false;
if (this.Session.ContainsKey("Model"))
{
    this._ModelField = ((global::Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General.IdentityDbContextModel)(this.Session["Model"]));
    ModelValueAcquired = true;
}
if ((ModelValueAcquired == false))
{
    string parameterValue = this.Host.ResolveParameterValue("Property", "PropertyDirectiveProcessor", "Model");
    if ((string.IsNullOrEmpty(parameterValue) == false))
    {
        global::System.ComponentModel.TypeConverter tc = global::System.ComponentModel.TypeDescriptor.GetConverter(typeof(global::Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General.IdentityDbContextModel));
        if (((tc != null) 
                    && tc.CanConvertFrom(typeof(string))))
        {
            this._ModelField = ((global::Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General.IdentityDbContextModel)(tc.ConvertFrom(parameterValue)));
            ModelValueAcquired = true;
        }
        else
        {
            this.Error("The type \'Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General.Identit" +
                    "yDbContextModel\' of the parameter \'Model\' did not match the type of the data pas" +
                    "sed to the template.");
        }
    }
}
if ((ModelValueAcquired == false))
{
    object data = global::Microsoft.DotNet.Scaffolding.Shared.T4Templating.CallContext.LogicalGetData("Model");
    if ((data != null))
    {
        this._ModelField = ((global::Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General.IdentityDbContextModel)(data));
    }
}

    }
}

    }
    #region Base class
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class IdentityDbContextBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        public System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}

