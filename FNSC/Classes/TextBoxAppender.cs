using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FNSC.Classes
{
    public class TextBoxAppender : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            string text = RenderLoggingEvent(loggingEvent);
        //    Form2.Instance?.LogToTextbox(text);
            
        }

    }
}
