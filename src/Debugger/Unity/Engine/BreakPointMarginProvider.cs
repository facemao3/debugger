using CodeEditor.Composition;
using CodeEditor.IO.Implementation;
using CodeEditor.Text.UI.Unity.Engine;

namespace Debugger.Unity.Engine
{
	[Export(typeof(ITextViewMarginProvider))]
	class BreakPointMarginProvider : ITextViewMarginProvider
	{
		[Import] private IBreakpointProvider _breakpointProvider;

		public ITextViewMargin MarginFor(ITextView textView)
		{
			return textView.Document.File is File ? CreateMargin(textView) : null;
		}

		private BreakPointMargin CreateMargin(ITextView textView)
		{
			return new BreakPointMargin(textView, _breakpointProvider);
		}
	}
}