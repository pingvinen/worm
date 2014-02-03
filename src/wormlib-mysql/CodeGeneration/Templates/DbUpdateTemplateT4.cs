﻿// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Worm.MySql.CodeGeneration.Templates {
    using Worm.CodeGeneration.Internals;
    using System;
    
    
    public partial class DbUpdateTemplateT4 : DbUpdateTemplateT4Base {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 4 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
 var idField = Model.Fields.GetPrimaryKeyField(); 
            
            #line default
            #line hidden
            
            #line 5 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("private ");
            
            #line default
            #line hidden
            
            #line 5 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.PocoClassName ));
            
            #line default
            #line hidden
            
            #line 5 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(" DbUpdate(IWormDbConnection db)\n{\n\tvar query = db.CreateQuery();\n\tvar columns = new List<string>();\n\n\t");
            
            #line default
            #line hidden
            
            #line 10 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
 foreach (PocoField f in Model.Fields.GetInsertFields()) { 
            
            #line default
            #line hidden
            
            #line 11 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("\n\tif (this.hasChanged_");
            
            #line default
            #line hidden
            
            #line 12 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( f.Name ));
            
            #line default
            #line hidden
            
            #line 12 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(")\n\t{\n\t\tcolumns.Add(\"`");
            
            #line default
            #line hidden
            
            #line 14 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( f.ColumnName ));
            
            #line default
            #line hidden
            
            #line 14 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("`=@");
            
            #line default
            #line hidden
            
            #line 14 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( f.Name ));
            
            #line default
            #line hidden
            
            #line 14 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("\");\n\t\tquery.AddParam(\"@");
            
            #line default
            #line hidden
            
            #line 15 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( f.Name ));
            
            #line default
            #line hidden
            
            #line 15 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("\", base.");
            
            #line default
            #line hidden
            
            #line 15 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( f.Name ));
            
            #line default
            #line hidden
            
            #line 15 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(");\n\t}\n\t");
            
            #line default
            #line hidden
            
            #line 17 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
 } 
            
            #line default
            #line hidden
            
            #line 18 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("\n\tif (columns.Count == 0)\n\t{\n\t\t// nothing has changed... no reason to bother the server\n\t\treturn this;\n\t}\n\n\tquery.Sql = String.Format(\"update `");
            
            #line default
            #line hidden
            
            #line 25 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( Model.TableName ));
            
            #line default
            #line hidden
            
            #line 25 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("` set {0} where `");
            
            #line default
            #line hidden
            
            #line 25 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( idField.ColumnName ));
            
            #line default
            #line hidden
            
            #line 25 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("`=@id limit 1\", String.Join(\", \", columns));\n\tquery.AddParam(\"@id\", base.");
            
            #line default
            #line hidden
            
            #line 26 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( idField.Name ));
            
            #line default
            #line hidden
            
            #line 26 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(");\n\n\tquery.ExecuteNonQuery();\n\t");
            
            #line default
            #line hidden
            
            #line 29 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
 foreach (PocoField f in Model.Fields.GetPublicFields()) { 
            
            #line default
            #line hidden
            
            #line 30 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("\n\tthis.hasChanged_");
            
            #line default
            #line hidden
            
            #line 31 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture( f.Name ));
            
            #line default
            #line hidden
            
            #line 31 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write(" = false;\n\t");
            
            #line default
            #line hidden
            
            #line 32 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
 } 
            
            #line default
            #line hidden
            
            #line 33 "/home/pingvinen/gitclones/me/worm/src/wormlib-mysql/Writing/Templates/DbUpdateTemplateT4.tt"
            this.Write("\n\treturn this;\n}\t\t");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        protected virtual void Initialize() {
        }
    }
    
    public class DbUpdateTemplateT4Base {
        
        private global::System.Text.StringBuilder builder;
        
        private global::System.Collections.Generic.IDictionary<string, object> session;
        
        private global::System.CodeDom.Compiler.CompilerErrorCollection errors;
        
        private string currentIndent = string.Empty;
        
        private global::System.Collections.Generic.Stack<int> indents;
        
        private ToStringInstanceHelper _toStringHelper = new ToStringInstanceHelper();
        
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session {
            get {
                return this.session;
            }
            set {
                this.session = value;
            }
        }
        
        public global::System.Text.StringBuilder GenerationEnvironment {
            get {
                if ((this.builder == null)) {
                    this.builder = new global::System.Text.StringBuilder();
                }
                return this.builder;
            }
            set {
                this.builder = value;
            }
        }
        
        protected global::System.CodeDom.Compiler.CompilerErrorCollection Errors {
            get {
                if ((this.errors == null)) {
                    this.errors = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errors;
            }
        }
        
        public string CurrentIndent {
            get {
                return this.currentIndent;
            }
        }
        
        private global::System.Collections.Generic.Stack<int> Indents {
            get {
                if ((this.indents == null)) {
                    this.indents = new global::System.Collections.Generic.Stack<int>();
                }
                return this.indents;
            }
        }
        
        public ToStringInstanceHelper ToStringHelper {
            get {
                return this._toStringHelper;
            }
        }
        
        public void Error(string message) {
            this.Errors.Add(new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message));
        }
        
        public void Warning(string message) {
            global::System.CodeDom.Compiler.CompilerError val = new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message);
            val.IsWarning = true;
            this.Errors.Add(val);
        }
        
        public string PopIndent() {
            if ((this.Indents.Count == 0)) {
                return string.Empty;
            }
            int lastPos = (this.currentIndent.Length - this.Indents.Pop());
            string last = this.currentIndent.Substring(lastPos);
            this.currentIndent = this.currentIndent.Substring(0, lastPos);
            return last;
        }
        
        public void PushIndent(string indent) {
            this.Indents.Push(indent.Length);
            this.currentIndent = (this.currentIndent + indent);
        }
        
        public void ClearIndent() {
            this.currentIndent = string.Empty;
            this.Indents.Clear();
        }
        
        public void Write(string textToAppend) {
            this.GenerationEnvironment.Append(textToAppend);
        }
        
        public void Write(string format, params object[] args) {
            this.GenerationEnvironment.AppendFormat(format, args);
        }
        
        public void WriteLine(string textToAppend) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendLine(textToAppend);
        }
        
        public void WriteLine(string format, params object[] args) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendFormat(format, args);
            this.GenerationEnvironment.AppendLine();
        }
        
        public class ToStringInstanceHelper {
            
            private global::System.IFormatProvider formatProvider = global::System.Globalization.CultureInfo.InvariantCulture;
            
            public global::System.IFormatProvider FormatProvider {
                get {
                    return this.formatProvider;
                }
                set {
                    if ((this.formatProvider == null)) {
                        throw new global::System.ArgumentNullException("formatProvider");
                    }
                    this.formatProvider = value;
                }
            }
            
            public string ToStringWithCulture(object objectToConvert) {
                if ((objectToConvert == null)) {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                global::System.Type type = objectToConvert.GetType();
                global::System.Type iConvertibleType = typeof(global::System.IConvertible);
                if (iConvertibleType.IsAssignableFrom(type)) {
                    return ((global::System.IConvertible)(objectToConvert)).ToString(this.formatProvider);
                }
                global::System.Reflection.MethodInfo methInfo = type.GetMethod("ToString", new global::System.Type[] {
                            iConvertibleType});
                if ((methInfo != null)) {
                    return ((string)(methInfo.Invoke(objectToConvert, new object[] {
                                this.formatProvider})));
                }
                return objectToConvert.ToString();
            }
        }
    }
}
