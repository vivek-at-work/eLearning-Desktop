using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrpter
{
    class Program
    {
        static void Main(string[] args)
        {
            string root = string.Empty; ;
            Console.WriteLine("Enter Directory Path Wihich contains un incrpted files only");
            string Superroot= Console.ReadLine();


            var files = Directory.GetFiles(Superroot);
            foreach (var f in files)
            {
                root = Path.Combine(new DirectoryInfo(Superroot).Parent.FullName , "encrpted");
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                

                Coneixement.Infrastructure.Helpers.FileEncryption.EncryptFile(f , Path.Combine(root , new FileInfo(f).Name));
            }


            Console.WriteLine("Collect Your encryptef files inside " + Environment.NewLine + root);
            Console.ReadKey();



           // foreach (var item1 in Directory.GetDirectories(Superroot))
	        //{
              //  if (new DirectoryInfo(item1).Name != "Encrypted")
                //{
                   // DirectoryInfo encrptedDirecotryRoot=null;
                    //root = item1;
                    //DirectoryInfo rootDirectory = new DirectoryInfo(root);

                    //if (!Directory.Exists(Path.Combine(rootDirectory.Parent.FullName, "Encrypted")))
                   // {
                     //   encrptedDirecotryRoot = Directory.CreateDirectory(Path.Combine(rootDirectory.Parent.FullName, "Encrypted"));
                    //}
                    //else
                    //{
                      //  encrptedDirecotryRoot = new DirectoryInfo(Path.Combine(rootDirectory.Parent.FullName, "Encrypted"));
                    //}

                    //DirectoryInfo encrptedsubjectDirecotryRoot = Directory.CreateDirectory(Path.Combine(encrptedDirecotryRoot.FullName, rootDirectory.Name));
                   //NOTES AND DESCRIPTION
                   /* var cd = Directory.GetDirectories(root);
                    foreach (var item in cd)
                    {
                        var topicroot = Directory.CreateDirectory(Path.Combine(encrptedsubjectDirecotryRoot.FullName, new DirectoryInfo(item).Name));
                        var files = Directory.GetFiles(item);
                        foreach (var file in files)
                        {
                            Coneixement.Infrastructure.Helpers.FileEncryption.EncryptFile(file, Path.Combine(topicroot.FullName, new FileInfo(file).Name));
                        }
                    }*/

                    //VIDEOS


                  //  foreach (var file in Directory.GetFiles(root))
                  //  {
                  ///      Coneixement.Infrastructure.Helpers.FileEncryption.EncryptFile(file, Path.Combine(encrptedsubjectDirecotryRoot.FullName, new FileInfo(file).Name));
                  //  }

                    //Test Series
               /*     var d = Directory.GetDirectories(root);
                    foreach (var item in d)
                    {
                       



                        var files = Directory.GetFiles(item);
                            foreach (var f in files)
                            {
                               var p= Path.Combine(encrptedsubjectDirecotryRoot.FullName, new DirectoryInfo(item).Name);
                               if (!Directory.Exists(p))
                               {
                                   Directory.CreateDirectory(p);
                               }

                                Coneixement.Infrastructure.Helpers.FileEncryption.EncryptFile(f, Path.Combine(p, new FileInfo(f).Name));
                            }

                        
                    }*/

                  

                }
	        }
           

        

        }
    //}
//}
