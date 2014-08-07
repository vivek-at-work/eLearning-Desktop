using Coneixement.Desktop;
using Coneixement.PDFViewer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coneixement.PDFViewer
{
    public class PdfViewer : IPDFViwerView
    {


     public   WPFPdfViewer.PdfViewer Reader
        {
            get;
            set;

        }
        public IViewModel ViewModel
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

    public PdfViewer( string Path)
        {



           
           
             
            Reader = new WPFPdfViewer.PdfViewer();
       
            Reader.ShowToolBar = false;
            Reader.LoadFile(Path);
    
           
        }

 
    }
}
