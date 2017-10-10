using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseFlatFile
{
    public class Program
    {
        static void Main(string[] args)
        {
            ParseAndSaveDataToDataBase();
        }

        public static void ParseAndSaveDataToDataBase()
        {
            string fileName = @"D:\ALL.csj";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fileName))
            {
                string line = "";
                String ProjectID = "";
                PIT_Stg_ParsingHdr entity = new PIT_Stg_ParsingHdr();

                while ((line = sr.ReadLine()) != null)
                {
                    //These are the fields that I have to parse from the flat file and save in to my database
                    if (line.Trim().Contains("COUNTY") && line.Trim().Contains("HIGHWAY") && line.Trim().Contains("LENGTH"))
                    {
                        string[] arr = line.Split(new string[] { "COUNTY:" }, StringSplitOptions.None);
                        string[] county = arr[1].Split(new string[] { "HIGHWAY:" }, StringSplitOptions.None);
                        string[] highway = arr[1].Split(new string[] { "LENGTH:" }, StringSplitOptions.None);
                        string[] highwayValue = highway[0].Split(new string[] { "HIGHWAY:" }, StringSplitOptions.None);
                        string[] length = arr[1].Split(new string[] { "LENGTH:" }, StringSplitOptions.None);
                        string[] lengthValue = length[1].Split(new string[] { "LENGTH:" }, StringSplitOptions.None);
                        entity.County = county[0].Trim().Replace("\0", string.Empty);
                        entity.Highway = highwayValue[1].Trim().Replace("\0", string.Empty);
                        entity.length = lengthValue[0].Trim().Replace("\0", string.Empty);
                    }
                    else if (line.Trim().Contains("CONTROL NUMBER") && line.Trim().Contains("DBE GOAL"))
                    {
                        string[] arr = line.Split(new string[] { "CONTROL NUMBER:" }, StringSplitOptions.None);
                        string[] controlNumber = arr[1].Split(new string[] { "DBE GOAL:" }, StringSplitOptions.None);
                        string controlNumberValue = controlNumber[0].Trim().Replace("\0", string.Empty);
                        string dbeGoalValue = controlNumber[1].Trim().Replace("\0", string.Empty);
                        Console.WriteLine(controlNumber[0].Trim().Replace("\0", string.Empty));
                        Console.WriteLine(controlNumber[1].Trim().Replace("\0", string.Empty));

                        entity.Control = controlNumberValue;
                        entity.DBEGoal = dbeGoalValue;
                    }
                    else if (line.Trim().Contains("CONTROL NUMBER") && line.Trim().Contains("SBE GOAL"))
                    {
                        string[] arr = line.Split(new string[] { "CONTROL NUMBER:" }, StringSplitOptions.None);
                        string[] controlNumber = arr[1].Split(new string[] { "SBE GOAL:" }, StringSplitOptions.None);
                        string controlNumberValue = controlNumber[0].Trim().Replace("\0", string.Empty);
                        string sbeGoalValue = controlNumber[1].Trim().Replace("\0", string.Empty);
                        Console.WriteLine(controlNumber[0].Trim().Replace("\0", string.Empty));
                        Console.WriteLine(controlNumber[1].Trim().Replace("\0", string.Empty));

                        entity.Control = controlNumberValue;
                        entity.SBEGoal = sbeGoalValue;
                    }
                    else if (line.Trim().Contains("PROJECT NUMBER:"))
                    {
                        string[] arr = line.Split(new string[] { "PROJECT NUMBER:" }, StringSplitOptions.None);
                        string[] projectNumber = arr[1].Split(new string[] { "PROJECT NUMBER:" }, StringSplitOptions.None);
                        Console.WriteLine(projectNumber[0].Trim().Replace("\0", string.Empty));

                        entity.ProjectNumber = projectNumber[0].Trim().Replace("\0", string.Empty);
                    }
                    else if (line.Trim().Contains("TYPE:"))
                    {
                        string[] arr = line.Split(new string[] { "TYPE:" }, StringSplitOptions.None);
                        string[] type = arr[1].Split(new string[] { "TYPE:" }, StringSplitOptions.None);
                        Console.WriteLine(type[0].Trim().Replace("\0", string.Empty));

                        entity.Type = type[0].Trim().Replace("\0", string.Empty);
                    }
                    else if (line.Trim().Contains("TIME FOR COMPLETION:") && line.Trim().Contains("GUARANTY:"))
                    {
                        string[] arr = line.Split(new string[] { "TIME FOR COMPLETION:" }, StringSplitOptions.None);
                        string[] timeForCompletion = arr[1].Split(new string[] { "GUARANTY:" }, StringSplitOptions.None);
                        string timeForCompletionValue = timeForCompletion[0].Trim().Replace("\0", string.Empty);
                        string guarantyValue = timeForCompletion[1].Trim().Replace("\0", string.Empty);
                        Console.WriteLine(timeForCompletionValue);
                        Console.WriteLine(guarantyValue);

                        entity.TimeForCompletion = timeForCompletionValue;
                        entity.Guaranty = guarantyValue;
                    }
                    else if (line.Trim().Contains("BIDS RECEIVED UNTIL:") && line.Trim().Contains("EST.COST:"))
                    {
                        string[] arr = line.Split(new string[] { "BIDS RECEIVED UNTIL:" }, StringSplitOptions.None);
                        string[] bidsReceivedUntil = arr[1].Split(new string[] { "EST.COST:" }, StringSplitOptions.None);
                        string bidsReceivedUntilValue = bidsReceivedUntil[0].Trim().Replace("\0", string.Empty);
                        string estCostValue = bidsReceivedUntil[1].Trim().Replace("\0", string.Empty);
                        Console.WriteLine(bidsReceivedUntilValue);
                        Console.WriteLine(estCostValue);

                        entity.BidsReceivedUntil = bidsReceivedUntilValue;
                        entity.EstimatedCost = estCostValue;
                    }
                    else if (line.Trim().Contains("BIDS WILL BE OPENED:"))
                    {
                        string[] arr = line.Split(new string[] { "BIDS WILL BE OPENED:" }, StringSplitOptions.None);
                        string IsbidsWillBeOpenedValue = arr[1].Trim().Replace("\0", string.Empty);
                        Console.WriteLine(IsbidsWillBeOpenedValue);

                        entity.BidsWillBeOpened = IsbidsWillBeOpenedValue;
                    }
                    else if (line.Trim().Contains("MAIL OR DELIVER BIDS TO:") && line.Trim().Contains("FOR PS&E PROJECT QUESTIONS CALL:"))
                    {
                        line = sr.ReadLine();
                        string mailDeliverAdd = "";
                        string projectQstnCall = "";
                        string[] AddressValuesArr = new string[2];

                        while (!line.Trim().Contains("FOR QUESTIONS REGARDING A PROPOSAL CALL") && !string.IsNullOrWhiteSpace(line))//&& !(x > 1)
                        {
                            int x = 0;
                            if (line.Trim().Contains("TO REQUEST A PROPOSAL GO TO"))
                            {
                                break;
                            }
                            if (line.Trim().Contains("FOR ELECTRONIC BIDDING VISIT THE WEBSITE BELOW"))
                            {
                                break;
                            }
                            else
                            {
                                string[] addressArr = line.Split(new string[] { "\0" }, StringSplitOptions.None);
                                for (int y = 0; y < addressArr.Length && !(x > 1); y++)
                                {
                                    if (!string.IsNullOrWhiteSpace(addressArr[y]) && addressArr[y] != "\0")
                                    {
                                        AddressValuesArr[x] = AddressValuesArr[x] + " " + addressArr[y];
                                        x++;
                                    }
                                }
                            }
                            line = sr.ReadLine();
                        }
                        if (!string.IsNullOrEmpty(AddressValuesArr[0]) || !string.IsNullOrEmpty(AddressValuesArr[1]))
                        {
                            mailDeliverAdd = AddressValuesArr[0];
                            projectQstnCall = AddressValuesArr[1];
                            entity.MailOrDeliverBidsTo = mailDeliverAdd.Trim().Replace("\0", string.Empty);
                            entity.PandSEContact = projectQstnCall.Trim().Replace("\0", string.Empty);
                        }
                    }
                    else if (line.Trim().Contains("FOR ELECTRONIC BIDDING VISIT THE WEBSITE BELOW:"))
                    {
                        line = sr.ReadLine();
                        string[] electronicBiddingWebsite = line.Split(new string[] { "FOR ELECTRONIC BIDDING VISIT THE WEBSITE BELOW:" }, StringSplitOptions.None);
                        string electronicBiddingWebsiteValue = "";
                        electronicBiddingWebsiteValue = electronicBiddingWebsite[0].Trim().Replace("\0", string.Empty);
                        using (var data = new ParseFlatFileEntity())
                        {
                            entity.Active = true;
                            entity.CreatedBy = "Basanta";
                            entity.LastModifiedBy = "Basanta";
                            entity.CreatedDate = DateTime.Now;
                            entity.LastModifiedDate = DateTime.Now;
                            entity.ElectronicBiddingWebsite = electronicBiddingWebsiteValue.TrimStart().Replace(" ", string.Empty);
                            data.PIT_Stg_ParsingHdr.Add(entity);
                            data.SaveChanges();
                            ProjectID = data.PIT_Stg_ParsingHdr.ToList().LastOrDefault().DataLoadID.ToString();
                            entity = new PIT_Stg_ParsingHdr();
                        }
                    }


                    // ITEMS READING STARTED
                    if (line.Trim().Contains("ALT") && line.Trim().Contains("NO."))
                    {
                        line = sr.ReadLine();
                        if (line.Contains("----"))
                        {
                            line = sr.ReadLine();
                        }
                        while (!line.Trim().Contains("COUNTY") && !line.Trim().Contains("HIGHWAY") && !line.Trim().Contains("LENGTH"))
                        {
                            if (line.Trim().Contains("INCLUDES FORCE ACCOUNT WORK AMOUNTS AS FOLLOWS") || line.Trim().Contains("CONTRACTOR FORCE ACCOUNT WORK") || line.Trim().Contains("THIS IS A WAIVED PROJECT - BIDDING PROPOSALS ISSUED TO PREQUALIFIED") || line.Trim().Contains("CONTRACTORS AND BIDDERS QUESTIONNAIRE CONTRACTORS UPON REQUEST") || line.Trim().Contains("MATERIAL FURNISHED BY THE STATE") || line.Trim().Contains("CONTRACTOR FORCE ACT OR AGR UNIT PRICE") || line.Trim().Contains("PUBLIC UTIL. FORCE ACCT WORK") || line.Trim().Contains("PLANS ARE REQUIRED") || line.Trim().Contains("THIS PROJECT INCLUDES PLANS") || line.Trim().Contains("NOTICE TO CONTRACTOR, PLANS ARE REQUIRED") || line.Trim().Contains("THIS PROJECT WAS PREVIOULY LET") || line.Trim().Contains("FOR BID PROPOSAL INFO"))
                            {
                                break;
                            }
                            else
                            {
                                PIT_Stg_ParsingItemDetail items = new PIT_Stg_ParsingItemDetail();
                                string appQnt = "";
                                string seq = "";
                                string UNIT = "";
                                string[] itemDetails = new string[8];
                                string[] itemsArr = line.Split(new string[] { "\0" }, StringSplitOptions.None);
                                int j = 0;
                                for (int i = 0; i < itemsArr.Length; i++)
                                {
                                    if (!string.IsNullOrEmpty(itemsArr[i]))
                                    {
                                        itemDetails[j] = itemsArr[i];
                                        j++;
                                    }
                                }
                                var itemLines = line.Split(' ');
                                if (itemLines.Length > 3)  // It's not a blank line
                                {
                                    string ALT = itemDetails[0].Split(new string[] { "\0" }, StringSplitOptions.None)[0];
                                    string NO = itemDetails[1].Split(new string[] { "\0" }, StringSplitOptions.None)[0];
                                    string CD = itemDetails[2].Split(new string[] { "\0" }, StringSplitOptions.None)[0];
                                    string ITEMDESCRIPTION = itemDetails[3].Split(new string[] { "\0" }, StringSplitOptions.None)[0];
                                    UNIT = itemDetails[4].Split(new string[] { "\0" }, StringSplitOptions.None)[0];
                                    appQnt = itemDetails[5].Split(new string[] { "\0" }, StringSplitOptions.None)[0];
                                    string bidPrice = "";
                                    string amount = "";
                                    seq = itemDetails[6].Split(new string[] { "\0" }, StringSplitOptions.None)[0];
                                    items.ALT = ALT.Trim();
                                    items.Amount = "";
                                    items.ApproximateQantities = appQnt.Trim();
                                    items.BidPrice = "";
                                    items.CreatedBy = "Basanta";
                                    items.CreatedDate = DateTime.Now;
                                    items.DataLoadID = int.Parse(ProjectID);
                                    items.DESCode = CD.Trim();
                                    items.ItemDescription = ITEMDESCRIPTION.Trim();
                                    items.ItemNO = NO.Trim();
                                    items.LastModifiedBy = "Basanta";
                                    items.LastModifiedDate = DateTime.Now;
                                    items.Seq = seq.Replace(" ", string.Empty).Trim();
                                    items.Unit = UNIT.Trim();
                                    using (var data = new ParseFlatFileEntity())
                                    {
                                        items.Active = true;
                                        data.PIT_Stg_ParsingItemDetail.Add(items);
                                        data.SaveChanges();
                                    }
                                }
                                line = sr.ReadLine();
                                while (line == " " || string.IsNullOrEmpty(line))
                                {
                                    line = sr.ReadLine();
                                }
                            }
                        }
                        if (line.Trim().Contains("COUNTY") && line.Trim().Contains("HIGHWAY") && line.Trim().Contains("LENGTH"))
                        {
                            string[] arr = line.Split(new string[] { "COUNTY:" }, StringSplitOptions.None);
                            string[] county = arr[1].Split(new string[] { "HIGHWAY:" }, StringSplitOptions.None);
                            string[] highway = arr[1].Split(new string[] { "LENGTH:" }, StringSplitOptions.None);
                            string[] highwayValue = highway[0].Split(new string[] { "HIGHWAY:" }, StringSplitOptions.None);
                            string[] length = arr[1].Split(new string[] { "LENGTH:" }, StringSplitOptions.None);
                            string[] lengthValue = length[1].Split(new string[] { "LENGTH:" }, StringSplitOptions.None);
                            entity.County = county[0].Trim().Replace("\0", string.Empty);
                            entity.Highway = highwayValue[1].Trim().Replace("\0", string.Empty);
                            entity.length = lengthValue[0].Trim().Replace("\0", string.Empty);
                        }
                    }
                }
            }
        }
    }
}
