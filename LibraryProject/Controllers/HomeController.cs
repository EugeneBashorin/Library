using LibraryProject.Extention_Classes;
using LibraryProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class HomeController : Controller
    {
        private const string _BOOK_KEY = "book";
        private const string _MAGAZINE_KEY = "magazine";
        private const string _NEWSPAPER_KEY = "newspaper";
        private const int _AUTOINCREMENT = 1;

        List<Book> bookList;
        List<Magazine> magazineList;
        List<NewsPaper> newsPaperList;
        IndexModel indexModel;

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["LibraryState"] == null)
            {
                indexModel = new IndexModel();
                bookList = new List<Book>();
                magazineList = new List<Magazine>();
                newsPaperList = new List<NewsPaper>();

                bookList.AddRange(new List<Book> { new Book { Name = "Head First C#", Author = "A.Stellman,J.Greene", Publisher = "OReilly", Price = 540, Id = 1 },
                    new Book{Name = "LINQ Succinctly", Author = "J.Roberts", Publisher = "Syncfusion", Price = 200, Id = 2},
                    new Book{Name = "Java Script Patterns", Author = "S.Stefanov", Publisher = "OReilly", Price = 312, Id = 3 },
                    new Book{Name = "Head First JS Programming", Author = "E.Freeman,E.Robson", Publisher = "OReilly", Price = 330, Id = 4 },
                    new Book{Name = "SQL The Complete Reference", Author = "J.Groff,P.Weinberg,A.Oppel", Publisher = "Williams", Price = 158, Id = 5 },
                    new Book{Name = "Getting started with ASP.NET 5.0 Web Forms", Author = "N.Gaylord,Ch.Wenz,P.Rastogi,T.Miranda", Publisher = "OReilly", Price = 400, Id = 6 },
                    new Book{Name = "C# 6.0 Complete Guide", Author = "J. & B.Albahairy", Publisher = "Williams", Price = 499, Id = 7 },
                    new Book{Name = "ASP.NET 4.5 in C# and VB", Author = "J.Gaylord", Publisher = "Wrox", Price = 274, Id = 8 },
                    new Book{Name = "Head First SQL", Author = "Lynn Beighley", Publisher = "OReilly", Price = 299, Id = 9 },
                    new Book{Name = "Design Patterns via C#", Author = "A.Shevchyk,A.Kasianov,D.Ohrimenko", Publisher = "ITVDN", Price = 830, Id = 10 },
                    new Book{Name = "OOP in C#. Succinctly", Author = "S.Rossel", Publisher = "Syncfusion", Price = 830, Id = 11}
                });

                magazineList.AddRange(new List<Magazine> { new Magazine{ Name = "MartialMix", Category = "Sport", Price = 16, Publisher = "Williams", Id = 1},
                    new Magazine{Name = "Fashion", Category = "Fashion", Price = 20, Publisher = "MagGroup",Id = 2},
                    new Magazine{Name = "Forbs", Category = "Economic", Price = 25, Publisher = "StanleyCo",Id = 3},
                    new Magazine{Name = "Geek", Category = "IT", Price = 21, Publisher = "StanleyCo",Id = 4},
                    new Magazine{Name = "Amaizing Wild World", Category = "Nature", Price = 22, Publisher = "StanleyCo",Id = 5},
                    new Magazine{Name = "Braine Scince", Category = "Psychology", Price = 26, Publisher = "Williams",Id = 6},
                    new Magazine{Name = "Car Evo", Category = "Car", Price = 24, Publisher = "MagGroup",Id = 7},
                    new Magazine{Name = "Robo", Category = "Scince", Price = 32, Publisher = "StanleyCo",Id = 8},
                    new Magazine{Name = "Zadrot", Category = "Games", Price = 16, Publisher = "Williams",Id = 9},
                    new Magazine{Name = "Design & Creative", Category = "Design", Price = 30, Publisher = "MagGroup",Id = 10}
                });

                newsPaperList.AddRange(new List<NewsPaper> {new NewsPaper{Name = "The NewYork Times", Category = "News", Price = 7, Publisher = "Williams", Id =1},
                    new NewsPaper{Name = "The WallSteet Jornal", Category = "Economy", Price = 7, Publisher = "Williams", Id = 2},
                    new NewsPaper{Name = "Ring", Category = "Sport", Price = 7, Publisher = "Ronald", Id = 3},
                    new NewsPaper{Name = "Los Angeles Times", Category = "News", Price = 7, Publisher = "WestCost", Id = 4},
                    new NewsPaper{Name = "The Washington Post", Category = "News", Price = 7, Publisher = "Croxy", Id = 5},
                    new NewsPaper{Name = "The Times", Category = "News", Price = 1, Publisher = "Croxy", Id = 6},
                    new NewsPaper{Name = "The Guardian", Category = "News", Price = 1, Publisher = "WestCost", Id = 7},
                    new NewsPaper{Name = "The Daily Telegraph", Category = "News", Price = 1, Publisher = "Croxy", Id = 8},
                    new NewsPaper{Name = "Financial Times", Category = "Economy", Price = 1, Publisher = "Williams", Id= 9},
                    new NewsPaper{Name = "Le Figaro", Category = "News", Price = 1, Publisher = "WestCost", Id= 10}
                });

                indexModel.Books = bookList;
                indexModel.Magazines = magazineList;
                indexModel.NewsPapers = newsPaperList;

                Session["LibraryState"] = indexModel;

            }
            if (Session["LibraryState"] != null)
            {
                indexModel = (IndexModel)Session["LibraryState"];
            }
            return View(indexModel);
        }

        [HttpPost]
        public ActionResult GetPublisherList(string id = "", string publisherName = "")
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            if (id == _BOOK_KEY)
            {
                List<Book> bookList = indexModel.Books;
                List<Book> newBookList = new List<Book>();
                Session["BookMemorry"] = bookList;
                foreach (var item in bookList)
                {
                    if (item.Publisher == publisherName)
                    {
                        newBookList.Add(item);
                    }
                }
                indexModel.Books = newBookList;
            }
            if (id == _MAGAZINE_KEY)
            {
                List<Magazine> magazineList = indexModel.Magazines;
                List<Magazine> newMagazineList = new List<Magazine>();
                Session["MagazineMemorry"] = magazineList;
                foreach (var item in magazineList)
                {
                    if (item.Publisher == publisherName)
                    {
                        newMagazineList.Add(item);
                    }
                }
                indexModel.Magazines = newMagazineList;
            }
            if (id == _NEWSPAPER_KEY)
            {
                List<NewsPaper> newspaperList = indexModel.NewsPapers;
                List<NewsPaper> newNewsPaperList = new List<NewsPaper>();
                Session["NewsPaperMemorry"] = newspaperList;
                foreach (var item in newspaperList)
                {
                    if (item.Publisher == publisherName)
                    {
                        newNewsPaperList.Add(item);
                    }
                }
                indexModel.NewsPapers = newNewsPaperList;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetAllPublisher(string id = "")
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            if (id == _BOOK_KEY)
            {
                indexModel.Books = (List<Book>)Session["BookMemorry"];
            }
            if (id == _MAGAZINE_KEY)
            {
                indexModel.Magazines = (List<Magazine>)Session["MagazineMemorry"];
            }
            if (id == _NEWSPAPER_KEY)
            {
                indexModel.NewsPapers = (List<NewsPaper>)Session["NewsPaperMemorry"];
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetBooksList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Book> bookList = indexModel.Books;
            bookList.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewsPapersList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<NewsPaper> newsPaperList = indexModel.NewsPapers;
            newsPaperList.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Magazine> MagazinesList = indexModel.Magazines;
            MagazinesList.GetTxtList();
            return RedirectToAction("Index");
        }

        public ActionResult GetBooksXmlList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Book> bookList = indexModel.Books;
            bookList.GetXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetNewspapersXmlList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<NewsPaper> newspaperList = indexModel.NewsPapers;
            newspaperList.GetXmlList();
            return RedirectToAction("Index");
        }

        public ActionResult GetMagazinesXmlList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Magazine> magazineList = indexModel.Magazines;
            magazineList.GetXmlList();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateBook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            book.Id = indexModel.Books.Count + _AUTOINCREMENT;
            indexModel.Books.Add(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowBook(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            Book book = (from t in indexModel.Books
                         where t.Id == id
                         select t).First();

            return View(book);
        }

        [HttpGet]
        public ActionResult EditBook(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            Book book = (from t in indexModel.Books
                         where t.Id == id
                         select t).First();
            return View(book);
        }

        [HttpPost]
        public ActionResult EditBook(Book newBook)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            foreach (Book book in indexModel.Books)
            {
                if (book.Id == newBook.Id)
                {
                    book.Name = newBook.Name;
                    book.Author = newBook.Author;
                    book.Price = newBook.Price;
                    book.Publisher = newBook.Publisher;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteBook(int? id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            Book book = (from t in indexModel.Books
                         where t.Id == id
                         select t).First();

            return PartialView(book);
        }

        [HttpPost, ActionName("DeleteBook")]
        public ActionResult DeleteConfirmedBook(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            indexModel.Books.Remove(indexModel.Books.Where(m => m.Id == id).First());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetDatabaseBookList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Book> bookList = indexModel.Books;
            bookList.SetBookListToDb();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetDatabaseMagazineList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<Magazine> magazineList = indexModel.Magazines;
            magazineList.SetMagazineListToDb();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetDatabaseNewspaperList()
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            List<NewsPaper> newspaperList = indexModel.NewsPapers;
            newspaperList.SetNewspaperListToDb();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateMagazine()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMagazine(Magazine magazine)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            magazine.Id = indexModel.Magazines.Count + _AUTOINCREMENT;
            indexModel.Magazines.Add(magazine);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowMagazine(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            Magazine magazine = (from t in indexModel.Magazines
                                 where t.Id == id
                                 select t).First();

            return View(magazine);
        }

        [HttpGet]
        public ActionResult EditMagazine(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            Magazine magazine = (from t in indexModel.Magazines
                                 where t.Id == id
                                 select t).First();
            return View(magazine);
        }

        [HttpPost]
        public ActionResult EditMagazine(Magazine newMagazine)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            foreach (Magazine magazine in indexModel.Magazines)
            {
                if (magazine.Id == newMagazine.Id)
                {
                    magazine.Name = newMagazine.Name;
                    magazine.Category = newMagazine.Category;
                    magazine.Price = newMagazine.Price;
                    magazine.Publisher = newMagazine.Publisher;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteMagazine(int? id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            Magazine newMagazine = (from t in indexModel.Magazines
                                    where t.Id == id
                                    select t).First();

            return PartialView(newMagazine);
        }

        [HttpPost, ActionName("DeleteMagazine")]
        public ActionResult DeleteConfirmedMagazine(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            indexModel.Magazines.Remove(indexModel.Magazines.Where(m => m.Id == id).First());
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateNewspaper()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewspaper(NewsPaper newsPaper)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            newsPaper.Id = indexModel.NewsPapers.Count + _AUTOINCREMENT;
            indexModel.NewsPapers.Add(newsPaper);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShowNewspaper(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            NewsPaper newspaper = (from t in indexModel.NewsPapers
                                   where t.Id == id
                                   select t).First();

            return View(newspaper);
        }

        [HttpGet]
        public ActionResult EditNewspaper(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            NewsPaper newspaper = (from t in indexModel.NewsPapers
                                   where t.Id == id
                                   select t).First();
            return View(newspaper);
        }

        [HttpPost]
        public ActionResult EditNewspaper(NewsPaper newNewspaper)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];

            foreach (NewsPaper newspaper in indexModel.NewsPapers)
            {
                if (newspaper.Id == newNewspaper.Id)
                {
                    newspaper.Name = newNewspaper.Name;
                    newspaper.Category = newNewspaper.Category;
                    newspaper.Price = newNewspaper.Price;
                    newspaper.Publisher = newNewspaper.Publisher;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteNewspaper(int? id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            NewsPaper newspaper = (from t in indexModel.NewsPapers
                                   where t.Id == id
                                   select t).First();

            return PartialView(newspaper);
        }

        [HttpPost, ActionName("DeleteNewspaper")]
        public ActionResult DeleteConfirmedNewspaper(int id)
        {
            IndexModel indexModel = (IndexModel)Session["LibraryState"];
            indexModel.NewsPapers.Remove(indexModel.NewsPapers.Where(m => m.Id == id).First());
            return RedirectToAction("Index");
        }
    }
}