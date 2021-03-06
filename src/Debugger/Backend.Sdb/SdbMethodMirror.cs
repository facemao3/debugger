using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using MDS=Mono.Debugger.Soft;

namespace Debugger.Backend.Sdb
{
	class SdbMethodMirror : Wrapper, IMethodMirror
	{
		private MDS.MethodMirror methodMirror { get { return obj as MDS.MethodMirror; } }
		private readonly List<ILocation> locations;
		private MethodDefinition metadata;

		public SdbMethodMirror (MDS.MethodMirror methodMirror)
			: base (methodMirror)
		{
			locations = new List<ILocation> (this.methodMirror.Locations.Select (SdbLocationFor));
		}

		public string Name
		{
			get { return methodMirror.Name; }
		}

		public string FullName
		{
			get { return methodMirror.Name; }
		}

		public IList<ILocation> Locations
		{
			get { return locations.ToArray (); }
		}

		public ITypeMirror DeclaringType
		{
			get { return Cache.Lookup<SdbTypeMirror> (methodMirror.DeclaringType); }
		}

		public ITypeMirror ReturnType
		{
			get { return Cache.Lookup<SdbTypeMirror>(methodMirror.ReturnType); }
		}

		private static ILocation SdbLocationFor (MDS.Location l)
		{
			return Cache.Lookup<SdbLocation> (l);
		}

		public MethodDefinition Metadata
		{
			get { return methodMirror.Metadata; }
		}

		public override int GetHashCode ()
		{
			return (DeclaringType.Assembly.FullName + "_" + DeclaringType.FullName + "_" + FullName).GetHashCode ();
		}

		public override bool Equals (object o)
		{
			var right = o as SdbMethodMirror;
			if (right == null)
				return false;
			return this.GetHashCode () == right.GetHashCode ();
		}
	}
}
