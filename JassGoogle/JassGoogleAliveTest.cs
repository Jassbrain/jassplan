using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.GData.Calendar;
using Google.GData.Extensions;
using Google.GData.AccessControl;
using Google.GData.Client;

namespace JassGoogle
{
    class JassGoogleAliveTest
    {


        static void Main(string[] args)
        {

            Console.Write("JassServerModel Sanity Test Starting\n");
            try
            {
FeedQuery query = new FeedQuery();
CalendarService service = new CalendarService("application-name");

// Set your credentials:
service.setUserCredentials("pablo.elustondo@gmail.com", "iswhatyou2!");

// Create the query object:
query.Uri = new Uri("http://www.google.com/calendar/feeds/pablo.elustondo@gmail.com/private/full");

// Tell the service to query:
AtomFeed calFeed = service.Query(query);



AtomEntry entry = new AtomEntry();
AtomPerson author = new AtomPerson(AtomPersonType.Author);
author.Name = "Pablo Elustondo";
author.Email = "pablo.elustondo@gmail.com";
entry.Authors.Add(author);
string fakeentryName = "Tennis with Beth" + DateTime.Now.ToString();
string retrievedEntryTitle = "";
entry.Title.Text = fakeentryName;
entry.Content.Content = "Meet for a quick lesson.";

string feedURL = "http://www.google.com/calendar/feeds/pablo.elustondo@gmail.com/private/full";

Uri postUri = new Uri(feedURL);

// Send the request and receive the response:

AtomEntry insertedEntry = service.Insert(postUri, entry);

FeedQuery myQuery = new FeedQuery(feedURL);
myQuery.Query = "Tennis";
AtomFeed myResultsFeed = service.Query(myQuery);
if (myResultsFeed.Entries.Count > 0)
{
    AtomEntry firstMatchEntry = myResultsFeed.Entries[0];
    retrievedEntryTitle = firstMatchEntry.Title.Text;

}

if (fakeentryName == retrievedEntryTitle)
{
    Console.WriteLine("SUCCESS: We did get the calendar entry we inserted before");
}
else
{
    Console.WriteLine("ERROR: We did not get the calendar entry we inserted before");
}


EventQuery qry = new EventQuery(feedURL);
EventFeed fd = service.Query(qry);


foreach (EventEntry thisentry in fd.Entries)
{
    Console.WriteLine("Title: " + thisentry.Title.Text + 
                    " Summary" + thisentry.Summary.Text +
                    " StartTime: " + thisentry.Times[0].StartTime +
                    " EndTime: " + thisentry.Times[0].EndTime
                    );
}

            }
            catch (Exception e)
            {

                Console.WriteLine("JassServerModel Sanity Test Error: " + e.Message);

            }

            Console.WriteLine("Press Enter to Exit\n");
            Console.ReadLine();
        }




        public static string CalId()
        {
            GCalendar cal = new GCalendar
           ("Google calendar name", "Google account name", "Google account password");
            return cal.CalId();
        } 

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public class Calendar
        {
            public DateTime dt { get; set; }
            public string tle { get; set; }
        }
        public class GCalendar
        {
            private const string CAL_URL =
        "https://www.Google.com/calendar/feeds/default/owncalendars/full";
            private const string CAL_TEMPLATE =
        "https://www.Google.com/calendar/feeds/{0}/private/full";
            private string g_CalUrl = null;
            private string g_CalId = null;
            private readonly CalendarService g_calService = null;
            private readonly string g_CalendarName;
            private readonly string g_UserName;
            private readonly string g_Password;
            public GCalendar(string cal_Name, string user_name, string user_password)
            {
                g_CalendarName = cal_Name;
                g_UserName = user_name;
                g_Password = user_password;
                g_calService = new CalendarService("Calendar");
            }
            public Calendar[] GetEvents()
            {
                try
                {

                        EventQuery qry = new EventQuery(g_CalUrl);
                        EventFeed fd = g_calService.Query(qry);
                        return (from EventEntry entry in fd.Entries
                                select new Calendar()
                                {
                                    dt = entry.Times[0].StartTime,
                                    tle = entry.Title.Text
                                }).ToArray();
                }
                catch (Exception)
                {
                    return new Calendar[0];
                }
            }
            private bool google_authentication()
            {
                g_calService.setUserCredentials(g_UserName, g_Password);
                return SaveCalIdAndUrl();
            }
            private bool SaveCalIdAndUrl()
            {
                CalendarQuery qry = new CalendarQuery();
                qry.Uri = new Uri(CAL_URL);
                CalendarFeed resultFeed = (CalendarFeed)g_calService.Query(qry);
                foreach (CalendarEntry entry in resultFeed.Entries)
                {
                    if (entry.Title.Text == g_CalendarName)
                    {
                        g_CalId = entry.Id.AbsoluteUri.Substring(63);
                        g_CalUrl = string.Format(CAL_TEMPLATE, g_CalId);
                        return true;
                    }
                }
                return false;
            }
            public string CalId()
            {
                return g_CalId;
            }

        }
    }
}
