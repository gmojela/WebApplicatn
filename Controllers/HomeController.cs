using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using WebApplicatn.Models;


namespace WebApplicatn.Controllers
{
    public class HomeController : Controller
    {
 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// gets all records 
        /// </summary>
        /// <returns> List<User> </returns>
        public IActionResult Index()
        {
            
           try
            {
                XElement xelement = XElement.Load("users_2.xml");
                IEnumerable<XElement> books = xelement.Elements();
           
                    List<User> _users = new List<User>();

                    var bks = from book in books where book.Attribute("name").Value == "user" select book;

                    foreach(var book in bks)
                    { 
                        User _user = new User();

                        var bookDescendants = book.Descendants();
                        foreach (var user in bookDescendants)
                        {
                            if (user.Name == "id")
                            {
                                _user.Id = user.Value;
                            }
                            if (user.Name == "name")
                            {
                                _user.Name = user.Value;
                            }
                            if (user.Name == "surname")
                            {
                                _user.Surname = user.Value;
                            }
                            if (user.Name == "cellNumber")
                            {
                                _user.CellNumber = user.Value;
                            }
                        }
                        _users.Add(_user);

                    }
                    return View(_users);
                    }catch(Exception ex)
                    {
                        return View(ex.Message);
                     }
            }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

            [HttpPost]
        public IActionResult Create(User _user)
        {
            try
            {
                XDocument doc = XDocument.Load("users_2.xml");
                // Create tags 

                XElement xuser = new XElement("user");
                XElement xid = new XElement("id", Guid.NewGuid());
                XElement xname = new XElement("name", _user.Name);
                XElement xsurname = new XElement("surname", _user.Surname);
                XElement xcell = new XElement("cellnumber", _user.CellNumber);

                //Add tag children
                doc.Root.Add(xuser);
                // Set user tag name and value
                xuser.SetAttributeValue("name", "user");
                
                xuser.Add(xid);
                xuser.Add(xname);
                xuser.Add(xsurname);
                xuser.Add(xcell);

                //save document
                doc.Save(@"users_2.xml");
                ViewBag.Message = "User Added";
        
                return View();
            }
            catch (Exception err)
            {
                return View(err.Message);
            }
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        /// <summary>
        /// Edits a single record
        /// </summary>
        /// <param name="_user"></param>
        /// <returns>Edition Confirmation</returns>
        [HttpPost]
        public IActionResult Edit(User _user)
        {
            try
            {

                // Load document
                XmlDocument doc = new XmlDocument();
                doc.Load("users_2.xml");

                //Find And Edit Record
                foreach (XmlNode xNode in doc.SelectNodes("db/user"))
                {
                    if (xNode.SelectSingleNode("id").InnerText == _user.Id)
                    {
                        xNode.SelectSingleNode("name").InnerText = _user.Name;
                        xNode.SelectSingleNode("surname").InnerText = _user.Surname;
                        xNode.SelectSingleNode("cellnumber").InnerText = _user.CellNumber;

                    }
                }

                doc.Save(@"users_2.xml");

                ViewBag.Message = "Edited";
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        /// <summary>
        /// Delete a single user
        /// </summary>
        /// <param name="_user"></param>
        /// <returns>Confirmation of action</returns>
        public IActionResult Delete(User _user)
        {
            try
            {

                // Load document
                XmlDocument doc = new XmlDocument();
                doc.Load("users_2.xml");

                //Find And Delete Record
                foreach(XmlNode xNode in doc.SelectNodes("db/user"))
                    if(xNode.SelectSingleNode("id").InnerText == _user.Id) xNode.ParentNode.RemoveChild(xNode);
                            
            
            doc.Save(@"users_2.xml");
            
            ViewBag.Message = "Deleted";
            return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}