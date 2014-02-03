using System;

namespace Worm.Generator.Templates
{
	public partial class WormDbClassTemplate
	{
		public virtual PocoEntity Model { get; set; }
		public virtual DbGetByIdOrDefaultTemplateBase DbGetByIdOrDefaultTemplate { get; set; }
		public virtual DbInsertTemplateBase DbInsertTemplate { get; set; }
		public virtual DbUpdateTemplateBase DbUpdateTemplate { get; set; }

		#region Populate for field
		public virtual string GetPopulateForField(PocoField field)
		{
			string def = String.Format("default({0})", field.Type);

			string typeLower = field.Type.ToLowerInvariant();

			if (typeLower.Equals("string"))
			{
				def = "String.Empty";
			}

			else if (field.IsEnum)
			{
				return String.Format("this.{0} = ({1})Enum.Parse(typeof({1}), dr.GetOrDefault<string>(\"{2}\", \"--\", true);", field.Name, field.Type, field.ColumnName);
			}

			return String.Format("this.{0} = dr.GetOrDefault<{1}>(\"{2}\", {3});", field.Name, field.Type, field.ColumnName, def);
		}
		#endregion

		#region Output method if exists
		public virtual bool OutputMethodIfExists(string methodName)
		{
			var method = this.Model.Methods.GetMethodByName(methodName);

			if (method == default(Method))
			{
				return false;
			}

			WriteLine(method.Signature);
			PushIndent("\t\t");
			WriteLine("{");
			PushIndent("\t");
			WriteLine(method.Body.Replace("\n", "\n"+CurrentIndent));
			PopIndent();
			WriteLine("}");

			return true;
		}
		#endregion
	}
}