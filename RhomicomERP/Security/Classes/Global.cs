using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Npgsql;
using cadmaFunctions;
using Microsoft.VisualBasic.Devices;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using SystemAdministration.Forms;
using SystemAdministration.Dialogs;
using System.Windows.Forms;

namespace SystemAdministration.Classes
{
    /// <summary>
    /// A  class containing variables and 
    /// functions we will like to call directly from 
    /// anywhere in the project without creating an instance first
    /// </summary>
    class Global
    {
        #region "CONSTRUCTOR..."
        public Global() { }
        #endregion

        #region "GLOBAL DECLARATION..."
        public static SystemAdministration mySecurity = new SystemAdministration();
        public static mainForm myNwMainFrm = null;
        public static string[] dfltPrvldgs = { "View System Administration", "View Users & their Roles",
		/*2*/"View Roles & their Priviledges", "View Registered Modules & their Priviledges", 
		/*4*/"View Security Policies", "View Server Settings", "View User Logins",
		/*7*/"View Audit Trail Tables", "Add New Users & their Roles", "Edit Users & their Roles",
    /*10*/"Add New Roles & their Priviledges", "Edit Roles & their Priviledges", 
		/*12*/"Add New Security Policies", "Edit Security Policies", "Add New Server Settings",   
		/*15*/"Edit Server Settings", "Set manual password for users", 
		/*17*/"Send System Generated Passwords to User Mails", 
		/*18*/"View SQL", "View Record History","Add/Edit Extra Info Labels","Delete Extra Info Labels" };
        public static string[] subGrpsWithExtrInfo = { "Organization's Details", "Divisions/Groups" };
        public static string[] mdlsWithExtrInfo = { "Organization Setup", "Organization Setup" };
        public static string[] dfltExtraInfoNames = { "0", "Motto", "0", "Vision", "0", "Mission",
  "1", "Motto", "1", "Vision", "1", "Mission"};
        public static string[] sysLovs = { "Benefits Types", "Relationship Types"
        , "Person Types-Further Details", "Countries", "Currencies", "Organisation Types"
        , "Divisions or Group Types", "Person Type Change Reasons", "Person Types"
        , "Qualification Types", "National ID Types", "Pay Frequencies",
    /*12*/"Benefits & Dues/Contributions Value Types", "Extra Information Labels",
    "Divisions Images Directory","Organization Images Directory","Person Images Directory"
   ,"Organisations","Divisions/Groups","Jobs","Chart of Accounts",
		/*21*/"Transaction Accounts","Parent Accounts","Active Users","Person Titles",
        "Gender","Marital Status", "Nationalities", "Active Persons","Sites/Locations",
        "Grades","Positions","Asset Accounts","Expense Accounts","Revenue Accounts",
        "Liability Accounts","Equity Accounts","Pay Items","Pay Item Values",
        "Working Hours","Gathering Types","Organisational Pay Scale",
        "Transactions Date Limit 1","Transactions Date Limit 2",
    "Budget Accounts","Banks","Bank Branches","Bank Account Types","Balance Items",
      "Non-Balance Items","Person Sets for Payments","Item Sets for Payments",
    "Audit Logs Directory",/*53*/"Reports Directory","System Modules",
      "LOV Names","User Roles","Pay Item Classifications","System Priviledges",
      /*59*/"Payment Means", "Allowed IP Address for Request Listener",
      /*61*/"CV Courses","Schools/Academic Institutions","Other Locations",
      /*64*/"Jobs/Professions/Occupations","Certificate Names","Languages",
      /*67*/"Hobbies","Interests","Conduct","Attitudes",
      /*71*/"Companies/Work Places","Customized Module Names","Allowed Person Types for Roles",
      /*74*/"Document Signatory Columns", "Attachment Document Categories",
      /*76*/"Types of Incorporation","List of Professional Services","Grade Names","Schools/Organisations/Institutions",
      /*80*/"Account Classifications","Employer's File No.","Person's Email Addresses",
      /*83*/"SMS API Parameters","Universal Resource Locators (URLs)","Asset Register", 
    /* 86 */ "Audit Trail Trackable Actions","Site Types","Vault Item States", "All Enabled Modules", "All Other General Setups"};
        public static string[] sysLovsDesc = {"Benefits Types", "Relationship Types"
        , "Further Details about the available person types", "Countries", "Currencies", "Organisation Types"
        , "Divisions or Group Types", "Person Type Change Reasons", "Person Types"
        , "Qualification Types", "National ID Types", "Pay Frequencies",
      "Benefits & Dues/Contributions Value Types", "Extra Information Labels",
      "Directory for keeping images from the div_groups_table",
      "Directory for keeping images coming from the org_details_table",
      "Directory for Storing Person's Images",
      "List of all organizations stored in the system",
      "List of all divisions/groups stored in the system",
        "List of all Jobs stored in the system",
        "List of all Accounts stored in the system",
        "List of all accounts transactions can be posted into",
        "List of all Parent Accounts in the system",
        "List of all users in the system",
        "Name Titles of Organization Persons", "Gender",
      "Marital Status","Nationalities","Active Persons",
      "List of all Sites/Locations","List of all Grades",
        "List of all Positions","Asset Accounts","Expense Accounts",
        "Revenue Accounts","Liability Accounts","Equity Accounts",
        "Pay Items","Pay Item Values","Working Hours","Gathering Types",
        "Organisational Pay Scale","Transactions Date Limit 1" ,
        "Transactions Date Limit 2","Budget Accounts","Banks",
      "Bank Branches","Bank Account Types","Balance Items",
      "Non-Balance Items","Person Sets for Payments",
      "Item Sets for Payments",
      "Audit Logs Directory","Reports Directory","System Modules","LOV Names","User Roles",
      "Pay Item Classifications","System Priviledges","Various Payment Means", "Allowed IP Address for Request Listener",
      /*61*/"CV Courses","Schools/Academic Institutions","Other Locations",
      /*64*/"Jobs/Professions/Occupations","Certificate Names","Languages",
      /*67*/"Hobbies","Interests","Conduct","Attitudes",
      /*71*/"Companies/Work Places","Customized Module Names","Allowed Person Types for Roles",
      /*74*/"Document Signatory Columns", "Attachment Document Categories",
      /*76*/"Types of Incorporation","List of Professional Services","Grade Names","Schools/Organisations/Institutions",
      /*80*/"Account Classifications","Employer's File No.","Person's Email Addresses",
      /*83*/"SMS API Parameters","Universal Resource Locators (URLs)","Asset Register", 
    /* 86 */ "Audit Trail Trackable Actions","Site Types","Vault Item States", "All Enabled Modules", "All Other General Setups"};
        public static string[] sysLovsDynQrys = {"", ""
        , "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
  "select distinct trim(to_char(org_id,'999999999999999999999999999999')) a, org_name b, '' c from org.org_details order by 2",
  "select distinct trim(to_char(div_id,'999999999999999999999999999999')) a, div_code_name b, '' c, org_id d from org.org_divs_groups order by 2",
        "select distinct trim(to_char(job_id,'999999999999999999999999999999')) a, job_code_name b, '' c, org_id d from org.org_jobs order by 2",
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_num || '.' || accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where org.does_prsn_hv_accnt_id({:prsn_id},accnt_id)>0 order by accnt_num",
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, (CASE WHEN prnt_accnt_id>0 THEN accnt_num || '.' || accnt_name || ' ('|| accb.get_accnt_num(prnt_accnt_id)||'.'||accb.get_accnt_name(prnt_accnt_id)|| ')' WHEN control_account_id>0 THEN accnt_num || '.' || accnt_name || ' ('|| accb.get_accnt_num(control_account_id)||'.'||accb.get_accnt_name(control_account_id)|| ')' ELSE accnt_num || '.' || accnt_name END) b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (is_prnt_accnt = '0' and is_enabled = '1' and is_net_income = '0' and has_sub_ledgers = '0' and org.does_prsn_hv_accnt_id({:prsn_id},accnt_id)>0) order by accnt_num",
    //and  is_retained_earnings= '0'
		"select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_num || '.' || accnt_name b, '' c, org_id d, accnt_type e, accnt_num f from accb.accb_chart_of_accnts where (is_prnt_accnt = '1' and org.does_prsn_hv_accnt_id({:prsn_id},accnt_id)>0) order by accnt_num",
        "select distinct trim(to_char(user_id,'999999999999999999999999999999')) a, user_name b, '' c FROM sec.sec_users WHERE (now() between to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND "+
  "to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS')) order by 1","","","","",
  "SELECT distinct local_id_no a, trim(title || ' ' || sur_name || "+
        "', ' || first_name || ' ' || other_names) b, '' c, org_id d " +
        "FROM prs.prsn_names_nos a order by local_id_no DESC",
  "select distinct trim(to_char(location_id,'999999999999999999999999999999')) a, REPLACE(location_code_name || '.' || site_desc, '.' || location_code_name,'') b, '' c, org_id d from org.org_sites_locations order by 2",
  "select distinct trim(to_char(grade_id,'999999999999999999999999999999')) a, grade_code_name b, '' c, org_id d from org.org_grades order by 2",
        "select distinct trim(to_char(position_id,'999999999999999999999999999999')) a, position_code_name b, '' c, org_id d from org.org_positions order by 2",
  "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_num || '.' || accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'A' and is_prnt_accnt = '0' and is_enabled = '1' and  is_retained_earnings= '0' and is_net_income = '0' and has_sub_ledgers = '0' and org.does_prsn_hv_accnt_id({:prsn_id},accnt_id)>0) order by accnt_num",
  "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_num || '.' || accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'EX' and is_prnt_accnt = '0' and is_enabled = '1' and  is_retained_earnings= '0' and is_net_income = '0' and has_sub_ledgers = '0' and org.does_prsn_hv_accnt_id({:prsn_id},accnt_id)>0) order by accnt_num",
  "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_num || '.' || accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'R' and is_prnt_accnt = '0' and is_enabled = '1' and  is_retained_earnings= '0' and is_net_income = '0' and has_sub_ledgers = '0' and org.does_prsn_hv_accnt_id({:prsn_id},accnt_id)>0) order by accnt_num",
  "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_num || '.' || accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'L' and is_prnt_accnt = '0' and is_enabled = '1' and  is_retained_earnings= '0' and is_net_income = '0' and has_sub_ledgers = '0' and org.does_prsn_hv_accnt_id({:prsn_id},accnt_id)>0) order by accnt_num",
  "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_num || '.' || accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (accnt_type = 'EQ' and is_prnt_accnt = '0' and is_enabled = '1' and  is_retained_earnings= '0' and is_net_income = '0' and has_sub_ledgers = '0' and org.does_prsn_hv_accnt_id({:prsn_id},accnt_id)>0) order by accnt_num",
  "select distinct trim(to_char(item_id,'999999999999999999999999999999')) a, item_code_name b, '' c, org_id d from org.org_pay_items order by 2",
  "select distinct trim(to_char(pssbl_value_id,'999999999999999999999999999999')) a, pssbl_value_code_name b, '' c, item_id d from org.org_pay_items_values order by 2",
        "select distinct trim(to_char(work_hours_id,'999999999999999999999999999999')) a, work_hours_name b, '' c, org_id d from org.org_wrkn_hrs order by 2",
        "select distinct trim(to_char(gthrng_typ_id,'999999999999999999999999999999')) a, gthrng_typ_name b, '' c, org_id d from org.org_gthrng_types order by 2",
  "","","",
        "select distinct trim(to_char(accnt_id,'999999999999999999999999999999')) a, accnt_num || '.' || accnt_name b, '' c, org_id d, accnt_num e from accb.accb_chart_of_accnts where (org.does_prsn_hv_accnt_id({:prsn_id},accnt_id)>0 and is_prnt_accnt = '0' and is_enabled = '1' and has_sub_ledgers = '0') order by accnt_num","","","",
  "select distinct trim(to_char(item_id,'999999999999999999999999999999')) a, item_code_name b, '' c, org_id d from org.org_pay_items where item_maj_type = 'Balance Item' order by item_code_name",
  "select distinct trim(to_char(item_id,'999999999999999999999999999999')) a, item_code_name b, '' c, org_id d, pay_run_priority e from org.org_pay_items where item_maj_type = 'Pay Value Item' order by pay_run_priority",
  "select distinct trim(to_char(prsn_set_hdr_id,'999999999999999999999999999999')) a, prsn_set_hdr_name b, '' c, org_id d from pay.pay_prsn_sets_hdr order by prsn_set_hdr_name",
  "select distinct trim(to_char(hdr_id,'999999999999999999999999999999')) a, itm_set_name b, '' c, org_id d from pay.pay_itm_sets_hdr order by itm_set_name"
  ,"","", "select distinct trim(to_char(module_id,'999999999999999999999999999999')) a, module_name b, '' c from sec.sec_modules order by module_name"
  , "select distinct trim(to_char(value_list_id,'999999999999999999999999999999')) a, value_list_name b, '' c from gst.gen_stp_lov_names order by value_list_name"
  , "select distinct trim(to_char(role_id,'999999999999999999999999999999')) a, role_name b, '' c from sec.sec_roles order by role_name","",
    "select distinct trim(to_char(prvldg_id,'999999999999999999999999999999')) a, prvldg_name || ' (' || sec.get_module_nm(module_id) || ')' b, '' c, prvldg_id d from sec.sec_prvldgs order by prvldg_id","","",
      /*61*/"","","",
      /*64*/"","","",
      /*67*/"","","","",
      /*71*/"","","","","","","",
      /*78*/"select distinct grade_code_name a, grade_code_name b, '' c, org_id d from org.org_grades order by 1",
      /*79*/"",
      /*80*/"","",
  "SELECT distinct REPLACE(email,',',';') a, trim(title || ' ' || sur_name || "+
        "', ' || first_name || ' ' || other_names || ' ('||local_id_no||')') b, '' c, org_id d, local_id_no e " +
        "FROM prs.prsn_names_nos a order by local_id_no",
    /*83*/"","",
    "Select '' || asset_id a, asset_code_name || ':' || asset_desc || ':' || asset_classification || ':' ||asset_category || ':' || tag_number b, '' c, org_id d from accb.accb_fa_assets_rgstr order by 2", 
    /* 86 */ "","","","",""};

        public static string[] pssblVals = { "0", "Loans",
        "Money amounts granted to staff to be paid later"
        ,"0", "Allowances", "Money amounts granted to staff"
        ,"0", "Leave", "Vacation Days allowed for employees"
        ,"1", "Father", "Biological Male Parent"
        ,"1", "Mother", "Biological Female Parent"
        ,"1", "Spouse", "Husband or Wife"
        ,"1", "Ex-Spouse", "Former Husband or wife"
        ,"1", "Son", "Biological Male Child"
        ,"1", "Daughter", "Biological Female Child"
        ,"1", "Uncle", "Uncle"
        ,"1", "Aunt", "Aunt"
        ,"1", "Nephew", "Nephew"
        ,"1", "Niece", "Niece"
        ,"1", "In-Law", "In-Law"
        ,"1", "Cousin", "Cousin"
        ,"1", "Friend", "Friend"
        ,"1", "Guardian", "Guardian"
        ,"1", "Grand-Father", "Grand-Father"
        ,"1", "Grand-Mother", "Grand-Mother"
        ,"1", "Step-Father", "Step-Father"
        ,"1", "Step-Mother", "Step-Mother"
        ,"1", "Step-Son", "Step-Son"
        ,"1", "Step-Daughter", "Step-Daughter"
        ,"2", "Permanent-Full Time", "Full Time permanent staff"
        ,"2", "Permanent-Part Time", "Part Time permanent staff"
        ,"2", "Contract-Full Time", "Full Time contract staff"
        ,"2", "Contract-Part Time", "Part Time contract staff"
        ,"3", "Ghana", "GH"
        ,"3", "South Africa", "SA"
        ,"3", "United States of America", "USA"
        ,"3", "United Kingdom", "UK"
        ,"4", "GHS", "Ghana Cedis ₵"
        ,"4", "JPY", "Japanese Yen ¥"
        ,"4", "USD", "US Dollars $"
        ,"4", "GBP", "British Pound £"
        ,"5", "School", "Place of tution and learning"
        ,"5", "Hotel", "Place where rooms are hired out to the public"
        ,"5", "Church", "Place of Worship"
        ,"5", "NGO", "Non-Governmental Organization"
        ,"5", "Company", "Company"
    ,"5", "Super Market", "Super Market"
    ,"5", "Mini Mart", "Mini Mart"
    ,"5", "Shop/Store", "Shop/Store"
    ,"5", "Boutique", "Boutique"
      ,"6", "Office", "Major Division under Department"
        ,"6", "Unit", "Division under Office"
        ,"6", "Department", "Major Division in an Organization"
        ,"6", "Wing", "Typically in churchs"
        ,"6", "Club", "Association"
        ,"6", "Association", "Welfare Group"
        ,"6", "Religious Denomination", "Religious group"
        ,"6", "Team", "Group for competitions"
    ,"6", "Shareholders", "Group for Shareholders"
    ,"6", "Board of Directors", "Group for Board of Directors"
    ,"6", "Pay/Remuneration", "Group for Workers' Salaries/Wages"
    ,"6", "Top Management", "Group for Top Management"
    ,"6", "Access Control Group", "Access Control Group"
        ,"7", "New Shareholder", "New Shareholder"
        ,"7", "Starting Director/Shareholder", "Starting Director/Shareholder"
        ,"7", "New Recruitment", "New staff"
        ,"7", "Re-Employment", "Old staff coming back"
        ,"7", "New Enrolment", "New Member"
        ,"7", "Re-Enrolment", "Old Member coming back"
        ,"7", "End of Contract", "Contract has ended duely"
        ,"7", "Appointment as Board Member", "Appointment as Board Member"
        ,"7", "Termination of Appointment", "Appointment Terminated"
        ,"7", "Dismissal", "Sacked"
        ,"7", "Compulsory Retirement", "Reached age Limit"
        ,"7", "Voluntary Retirement", "Decided to retire early"
        ,"7", "Retirement on Medical Grounds", "Retiring due to Ailment"
        ,"7", "Change of Membership Terms", "Change of Membership Terms"
        ,"7", "Change of Employement Terms", "Change of Employement Terms"
        ,"8", "Shareholder", "Owner of Shares in the Company"
        ,"8", "Board Member", "Member of Board of Directors"
        ,"8", "Contact Person", "Relative or Friend"
        ,"8", "Ex-Contact Person", "Former Relative or Friend"
        ,"8", "Customer", "Client"
        ,"8", "Ex-Customer", "Former Client"
        ,"8", "Supplier", "Supplier of goods and services"
        ,"8", "Ex-Supplier", "Former Supplier of goods and services"
        ,"8", "Ex-Customer", "Former Client"
        ,"8", "Student", "Currently a Student"
        ,"8", "Old Student", "Former Student"
        ,"8", "Employee", "Currently a worker"
        ,"8", "Ex-Employee", "Former Worker"
        ,"8", "Staff", "Currently a worker"
        ,"8", "Ex-Staff", "Former Worker"
        ,"8", "Member", "Currently a Member of the group"
        ,"8", "Ex-Member", "A Former Member of the group"
        ,"9", "1st Degree", "First Degree University"
        ,"9", "2nd Degree", "Second Degree University"
    ,"9", "Form 5", "Senior Secondary School Cert.(O-Level)"
    ,"9", "Sixth Form", "Senior Secondary School Cert.(A-Level)"
        ,"9", "Senior High", "Senior High School Cert.(WASSCE)"
        ,"9", "Junior High", "Junior High School Cert.(BECE)"
        ,"9", "Phd", "Doctor of Philosophy"
        ,"10", "NHIS ID", "Health Insurance"
        ,"10", "Voter's ID", "Voter's ID Card"
        ,"10", "Driving License", "Driver's License"
        ,"10", "Passport", "Passport"
        ,"10", "SSNIT", "SSNIT"//, , , , , , , , 
	 ,"11", "fixed", "for payments at end of contracts"
     ,"11", "hourly", "hourly"
     ,"11", "daily", "daily"
     ,"11", "weekly", "weekly"
     ,"11", "fortnightly", "fortnightly"
     ,"11", "semi-month", "semi-month"
     ,"11", "month", "month"
     ,"11", "yearly", "yearly"
        ,"11", "quaterly", "quaterly"
        ,"12", "Money", "Money"
     ,"12", "Items", "Items"
     ,"12", "Service", "Service"
     ,"12", "Working Days", "Working Days"
     ,"13", "Motto", "Motto of a Group/Division"
     ,"13", "Mission", "Mission of a Group/Division"
     ,"13", "Vision", "Vision of a Group/Division"
     ,"13", "Vessel", "Shipping Vessel"
     ,"13", "Voyage", "Voyage Number"
     ,"13", "ATA", "ATA"
     ,"13", "ATS", "ATS"
     ,"13", "Port of Loading", "Port of Loading"
     ,"13", "Port of Discharge", "Port of Discharge"
   //,"14", Application.StartupPath + @"\Images\Divs", "Divisions Logos Directory"
   //,"15", Application.StartupPath + @"\Images\Org", "Organizations Logos Directory"
   //,"16", Application.StartupPath + @"\Images\Person", "Persons Images Directory"
		,"24","Mr.","Mr."
        ,"24","Mrs.","Mrs."
        ,"24","Master","Master"
        ,"24","Ms.","Ms."
        ,"24","Miss.","Miss."
        ,"24","Dr.","Dr."
        ,"24","Prof.","Prof."
        ,"25","Male","Male"
        ,"25","Female","Female"
  ,"25","Not Applicable","Not Applicable"
        ,"26","Single","Single"
        ,"26","Married","Married"
        ,"26","Divorced","Divorced"
        ,"26","Separated","Separated"
    ,"26","Widow","Widow"
    ,"26","Widower","Widower"
        ,"27","Ghanaian","Ghanaian"
        ,"27","American","American"
        ,"27","British","British"
        ,"27","Togolese","Togolese"
    ,"41", "2400","9999.Rhomicom Basic Worker Grade.P1"
    ,"41", "3000","9999.Rhomicom Basic Worker Grade.P2"
        ,"42", "01-JAN-1900 00:00:00","01-JAN-1900"
        ,"43","31-DEC-4000 23:59:59","31-DEC-4000"
  ,"45","Bank of Ghana","Bank of Ghana"
  ,"45","Barclays Bank","Barclays Bank"
  ,"45","Standard Chartered Bank","Standard Chartered Bank"
  ,"45","Ghana Commercial Bank","Ghana Commercial Bank"
  ,"45","Prudential Bank","Prudential Bank"
  ,"46","Accra Branch","Accra Branch"
  ,"46","Makola Branch","Makola Branch"
  ,"46","Ring Road Branch","Ring Road Branch"
  ,"46","Kaneshie Branch","Kaneshie Branch"
  ,"46","KNUST Branch","KNUST Branch"
  ,"47","Current Account","Kaneshie Branch"
  ,"47","Savings Account","KNUST Branch"
  //,"52", Application.StartupPath + @"\Images\Logs", "Audit Logs Directory"
  //,"53", Application.StartupPath + @"\Images\Rpts", "Reports Directory"
  ,"57","Payslip Item","Payslip Items-Items that appear on Payslip after during payroll run"
  ,"57","Payroll Item","Payroll Items-Items Run during payroll run but don't appear on Payslip"
  ,"57","Bill Item","Bill Items Eg. School Fees Bill Items"
  ,"57","Balance Item","Balance Items Eg. TAKE HOME PAY"
  ,"59","Cash Cheque","Cash Cheque"
  ,"59","Clearing Cheque","Clearing Cheque"
  ,"59","Payment Order","Payment Order"
  ,"59","Visa Card","Visa Card"
  ,"59","Mastercard","Mastercard"
  ,"59","Ezwich","Ezwich"
  ,"59","Visa Ghana","Visa Ghana"
  ,"59","Paypal","Paypal"
  ,"59","Mobile Money","Mobile Money",
  "59","Supplier Cheque", "Supplier Cheque",
  "59","Supplier Cash", "Supplier Cash",
  "59","Customer Cheque", "Customer Cheque",
  "59","Customer Cash","Customer Cash",
  "59","Supplier Prepayment Application","Supplier Prepayment Application",
  "59","Customer Prepayment Application","Customer Prepayment Application",
  "60","192.168.0.100","192.168.0.100",
  "60","localhost","localhost",
  "61","Bsc. Computer Science","Computer Science Degree",
  "61","Bsc. Computer Engineering","Computer Engineering Degree",
  "61","B.E.C.E","B.E.C.E",
  "61","W.A.S.S.C.E","W.A.S.S.C.E",
  "61","S.S.C.E","S.S.C.E",
  "61","A-Level","A-Level",
  "61","O-Level","O-Level",
  "62","Kwame Nkrumah University of Science and Technology","Tertiary",
  "62","University of Ghana-Legon","Tertiary",
  "62","Prempeh College","Secondary",
  "63","Accra-Ghana","Accra-Ghana",
  "63","Kumasi-Ghana","Kumasi-Ghana",
  "64","Engineer","Engineer",
  "64","IT Technician","IT Technician",
  "65","B.E.C.E","B.E.C.E",
  "65","W.A.S.S.C.E","W.A.S.S.C.E",
  "65","S.S.C.E","S.S.C.E",
  "65","A-Level","A-Level",
  "65","O-Level","O-Level",
  "65","Bsc.","Bachelor of Science",
  "65","Msc.","Master of Science",
  "65","PhD","Doctor of Philosophy",
  "66","Twi","Twi",
  "66","English","English",
  "67","Playing Soccer","Playing Soccer",
  "67","Playing Lead Guitar","Playing Lead Guitar",
  "68","Singing","Singing",
  "68","Reading","Reading",
  "69","Calm","Calm",
  "69","Respectful","Respectful",
  "70","Hardworking","Hardworking",
  "70","Serious","Serious",
  "71","Rhomicom Systems Tech. Ltd.","Rhomicom Systems Tech. Ltd.",
  "72","Basic Person Data","Personnel/ Membership Data",
  "72","Internal Payments","Personnel/ Membership Payments",
  "72","Learning/Performance Management", "Performance Management System",
  "72","Hospitality Management", "Hospitality Management",
  "72","Events and Attendance", "Events and Attendance",
  "72","Sales and Inventory", "Sales and Inventory",
  "72","Project Management", "Projects Management",
  "73","Basic Person Data Administrator", "'All'",
  "73","Personnel Data Administrator", "'Employee','Staff'",
  "74","Invoices Signatories", "                    Prepared By                    Authorized By                    Approved By".ToUpper(),
  "74","PO Signatories", "                    Prepared By                    Authorized By                    Approved By".ToUpper(),
  "74","Receipt Signatories", "                    Received By                    Inspected By                    Approved By".ToUpper(),
  "74","Receipt Return Signatories", "                    Returned By                    Authorized By                    Approved By".ToUpper(),
  "74","Payroll Signatories", "                    Prepared By                    Authorized By                    Approved By".ToUpper(),
  "75","Curriculum Vitae", "Curriculum Vitae",
  "75","Membership Forms", "Membership Forms",
  "75","Career Report", "Career Report",
  "75","Other Information", "Other Information",
  "76","Public Company Ltd", "Public Company Ltd",
  "76","Private Company Ltd", "Private Company Ltd",
  "76","Closed Corporation", "Closed Corporation",
  "76","Joint Venture", "Joint Venture",
  "76","Consortium", "Consortium",
  "76","Partnership", "Partnership",
  "76","Sole Proprietor", "Sole Proprietor",
  "76","Foreign Company", "Foreign Company",
  "76","Government/Parastatals", "Government/Parastatals",
  "76","Trust", "Trust",
  "77","Architecture", "Architecture",
  "77","Surveying", "Surveying",
  "77","Project Management", "Project Management",
  "77","Planning", "Planning",
  "77","Engineering", "Engineering",
  "79","Kwame Nkrumah University of Science and Technology","School",
  "79","University of Ghana-Legon","School",
  "79","Prempeh College","School",
  "79","Rhomicom Systems Tech. Ltd.","Company",
   "80","Cash and Cash Equivalents","Cash and Cash Equivalents",
    "80","Operating Activities.Sale of Goods","Operating Activities.Sale of Goods",
    "80","Operating Activities.Sale of Services","Operating Activities.Sale of Services",
    "80","Operating Activities.Other Income Sources","Operating Activities.Other Income Sources",
    "80","Operating Activities.Cost of Sales","Operating Activities.Cost of Sales",
    "80","Operating Activities.Net Income","Operating Activities.Net Income",
    "80","Operating Activities.Depreciation Expense","Operating Activities.Depreciation Expense",
    "80","Operating Activities.Amortization Expense","Operating Activities.Amortization Expense",
    "80","Operating Activities.Gain on Sale of Equipment","Operating Activities.Gain on Sale of Equipment"/*NEGATE*/,
    "80","Operating Activities.Loss on Sale of Equipment","Operating Activities.Loss on Sale of Equipment",
    "80","Operating Activities.Other Non-Cash Expense","Operating Activities.Other Non-Cash Expense",
    "80","Operating Activities.Accounts Receivable","Operating Activities.Accounts Receivable"/*NEGATE*/,
    "80","Operating Activities.Bad Debt Expense","Operating Activities.Bad Debt Expense"/*NEGATE*/,
    "80","Operating Activities.Prepaid Expenses","Operating Activities.Prepaid Expenses"/*NEGATE*/,
    "80","Operating Activities.Inventory","Operating Activities.Inventory"/*NEGATE*/,
    "80","Operating Activities.Accounts Payable","Operating Activities.Accounts Payable",
    "80","Operating Activities.Accrued Expenses","Operating Activities.Accrued Expenses",
    "80","Operating Activities.Taxes Payable","Operating Activities.Taxes Payable",
    "80","Operating Activities.Operating Expense","Operating Activities.Operating Expense",
    "80","Operating Activities.General and Administrative Expense","Operating Activities.General and Administrative Expense",
    "80","Investing Activities.Asset Sales/Purchases","Investing Activities.Asset Sales/Purchases"/*NEGATE*/,
    "80","Investing Activities.Equipment Sales/Purchases","Investing Activities.Equipment Sales/Purchases"/*NEGATE*/,
    "80","Financing Activities.Capital/Stock","Financing Activities.Capital/Stock",
    "80","Financing Activities.Long Term Debts","Financing Activities.Long Term Debts",
    "80","Financing Activities.Short Term Debts","Financing Activities.Short Term Debts",
    "80","Financing Activities.Equity Securities","Financing Activities.Equity Securities",
    "80","Financing Activities.Dividends Declared","Financing Activities.Dividends Declared"/*NEGATE*/,
    "80","","",
   "81","TIN","LEE 12345",
   "83","url", "http://txtconnect.co/api/send/",
   "83","token","123456789",
   "83","msg","{:msg}",
   "83","from","Rhomicom",
   "83","to","{:to}",
   "84","QR Code Validation URL","https://www.rhomicomgh.com/index.php?id=",
    "86", "INSERT STATEMENTS", "INSERT STATEMENTS",
    "86", "UPDATE STATEMENTS", "UPDATE STATEMENTS",
    "86", "DELETE STATEMENTS", "DELETE STATEMENTS",
    "86", "INFO ON DATA VIEWED", "INFO ON DATA VIEWED",
    "87", "Branch", "Branch",
    "87", "Agency", "Agency",
    "88", "Examined-Fit", "Examined-Fit",
    "88", "Examined-Unfit", "Examined-Unfit",
    "88", "Issuable", "Issuable",
    "88", "Mint", "Mint",
    "88", "Stack(Non-Mint)", "Stack(Non-Mint)",
    "88", "Unexamined", "Unexamined",
    "89", "Accounting", "Accounting",
    "89", "Basic Person Data", "Basic Person Data",
    "89", "Internal Payments", "Internal Payments",
    "89", "Stores And Inventory Manager", "Stores And Inventory Manager",
    "89", "Events And Attendance", "Events And Attendance",
    "89", "Hospitality Management", "Hospitality Management",
    "89", "Visits and Appointments", "Visits and Appointments",
    "89", "Projects Management", "Projects Management",
    "89", "Learning/Performance Management", "Learning/Performance Management",
    "89", "Generic Module", "Generic Module",
    "89", "General Setup", "General Setup",
    "89", "Organization Setup", "Organisation Setup",
    "89", "Reports And Processes", "Reports And Processes",
    "89", "System Administration", "System Administration",
    "90", "Allow Multiple Same User Logons", "Yes"
};

        public static string currentPanel = "";
        #endregion

        #region "DATA MANIPULATION FUNCTIONS..."
        #region "INSERT STATEMENTS..."
        public static void createAllwdExtraInfos(long tblID, int pssblValID,
         bool isEnbld)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_allwd_other_infos(" +
                     "table_id, other_info_id, is_enabled, created_by, " +
                     "creation_date, last_update_by, last_update_date) " +
             "VALUES (" + tblID + ", " + pssblValID + ", '" +
             Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(isEnbld) + "', " + Global.mySecurity.user_id +
             ", '" + dateStr + "', " + Global.mySecurity.user_id +
             ", '" + dateStr + "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void asgnRoleSetToUser(long usrID, int roleID,
      string in_strDte, string in_endDte)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string endDate = "4000-12-31 23:59:59";
            if (in_strDte.Length < 19)
            {
                in_strDte = dateStr;
            }
            else
            {
                in_strDte = DateTime.ParseExact(
            in_strDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (in_endDte.Length < 19)
            {
                in_endDte = endDate;
            }
            else
            {
                in_endDte = DateTime.ParseExact(
            in_endDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sqlStr = "INSERT INTO sec.sec_users_n_roles(" +
                              "user_id, role_id, valid_start_date, valid_end_date, created_by, " +
                              "creation_date, last_update_by, last_update_date) " +
              "VALUES (" + usrID + ", " + roleID + ", '" + in_strDte + "', '" + in_endDte + "', " +
              Global.mySecurity.user_id + ", '" + dateStr + "', " + Global.mySecurity.user_id + ", '" + dateStr + "')"; ;
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void createUser(string username, long ownrID,
          string in_strDte, string in_endDte, string pwd,
          long cstmrID, string modulesNeeded, string usrCode4Trns)
        {
            if (username.Length > 50)
            {
                username = username.Substring(0, 50);
            }
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string endDate = "4000-12-31 23:59:59";
            if (in_strDte.Length < 19)
            {
                in_strDte = dateStr;
            }
            else
            {
                in_strDte = DateTime.ParseExact(
            in_strDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (in_endDte.Length < 19)
            {
                in_endDte = endDate;
            }
            else
            {
                in_endDte = DateTime.ParseExact(
            in_endDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sqlStr = "INSERT INTO sec.sec_users(usr_password, person_id, is_suspended, is_pswd_temp, " +
              "failed_login_atmpts, user_name, last_login_atmpt_time, last_pswd_chng_time, valid_start_date, " +
              "valid_end_date, created_by, creation_date, last_update_by, last_update_date, customer_id, modules_needed, code_for_trns_nums) " +
              "VALUES (md5('" + Global.myNwMainFrm.cmmnCode.encrypt(pwd, CommonCode.CommonCodes.AppKey) + "'), " + ownrID + ", FALSE, TRUE, 0, '" +
              username.Replace("'", "''") + "', '" + dateStr + "', '" + dateStr + "', '" + in_strDte + "', '" + in_endDte +
              "', " + Global.mySecurity.user_id + ", '" + dateStr + "', " + Global.mySecurity.user_id + ", '" + dateStr + "', " + cstmrID +
              ", '" + modulesNeeded.Replace("'", "''") + "', '" + usrCode4Trns.Replace("'", "''") + "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void createRole(string rolename,
          string in_strDte, string in_endDte, bool canAssign)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string endDate = "4000-12-31 23:59:59";
            if (in_strDte.Length < 19)
            {
                in_strDte = dateStr;
            }
            else
            {
                in_strDte = DateTime.ParseExact(
            in_strDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (in_endDte.Length < 19)
            {
                in_endDte = endDate;
            }
            else
            {
                in_endDte = DateTime.ParseExact(
            in_endDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sqlStr = "INSERT INTO sec.sec_roles(role_name, valid_start_date, " +
              "valid_end_date, created_by, creation_date, last_update_by, last_update_date, can_mini_admins_assign) " +
              "VALUES ('" + rolename.Replace("'", "''") + "', '" + in_strDte + "', '" + in_endDte +
              "', " + Global.mySecurity.user_id + ", '" + dateStr + "', " + Global.mySecurity.user_id +
              ", '" + dateStr + "', '" + Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(canAssign) + "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void createPolicy(string plcy_nm, int faild_lgns
          , int expry_days, int auto_unlck, bool rqr_caps, bool rqr_smll, bool rqr_dgts, bool rqr_wild
          , string rgrmnt_cmdntns, bool is_dflt, int old_pwd_cnt, int min_pwd_len, int max_pwd_len
          , int max_no_recs, bool allw_rptn, bool allw_unm, decimal sessn_tme)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_security_policies(" +
                  "policy_name, max_failed_lgn_attmpts, pswd_expiry_days, " +
                  "auto_unlocking_time_mins, pswd_require_caps, pswd_require_small, " +
                  "pswd_require_dgt, pswd_require_wild, pswd_reqrmnt_combntns, is_default, " +
                  "created_by, creation_date, last_update_by, last_update_date, " +
                  "old_pswd_cnt_to_disallow, pswd_min_length, pswd_max_length, max_no_recs_to_dsply, " +
                  "allow_repeating_chars, allow_usrname_in_pswds, session_timeout) " +
              "VALUES ('" + plcy_nm.Replace("'", "''") + "', " + faild_lgns + ", " + expry_days + ", " +
                              "" + auto_unlck + ", " + rqr_caps + ", " + rqr_smll + ", " +
                              "" + rqr_dgts + ", " + rqr_wild + ", '" + rgrmnt_cmdntns.Replace("'", "''") + "', " + is_dflt + ", " +
                              "" + Global.mySecurity.user_id + ", '" + dateStr + "', " + Global.mySecurity.user_id + ", '" + dateStr + "', " +
                              "" + old_pwd_cnt + ", " + min_pwd_len + ", " + max_pwd_len + ", " + max_no_recs + ", " +
                              "" + allw_rptn + ", " + allw_unm + ", " + sessn_tme + ")";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void createEml_Svr(string smtp_nm, string mail_unme
          , string mail_pwd, int port_no, bool is_dflt, string actv_drctry,
          string ftpserver, string ftpunm, string ftppswd, int ftpport,
          string ftpdir, bool enfc,
          string pgdir, string bckpdir, string comPrt, string baudRate,
            string timeOut, string ftpHomeDir, string smtpIP)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_email_servers(" +
                              "smtp_client, mail_user_name, mail_password, " +
                              "smtp_port, is_default, actv_drctry_domain_name, " +
                              "created_by, creation_date, last_update_by, last_update_date, " +
                              "ftp_server_url, ftp_user_name, ftp_user_pswd, " +
                              "ftp_port, ftp_app_sub_directory, enforce_ftp, " +
                  "pg_dump_dir, backup_dir, com_port, baud_rate, timeout, ftp_user_start_directory, inhouse_smtp_ip) " +
              "VALUES ('" + smtp_nm.Replace("'", "''") + "', '" + mail_unme.Replace("'", "''") +
              "', '" + Global.myNwMainFrm.cmmnCode.encrypt1(mail_pwd, CommonCode.CommonCodes.AppKey).Replace("'", "''") + "', " +
                              "" + port_no + ", " + is_dflt + ", '" + actv_drctry.Replace("'", "''") + "', " +
                              "" + Global.mySecurity.user_id + ", '" + dateStr + "', " +
                              Global.mySecurity.user_id + ", '" + dateStr +
                              "', '" + ftpserver.Replace("'", "''") + "', '" + ftpunm.Replace("'", "''") +
                              "', '" + Global.myNwMainFrm.cmmnCode.encrypt1(ftppswd, CommonCode.CommonCodes.AppKey).Replace("'", "''") + "', " + ftpport +
                              ", '" + ftpdir.Replace("'", "''") + "', '" +
                              Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(enfc) + "','" + pgdir.Replace("'", "''") +
                              "','" + bckpdir.Replace("'", "''") + "','" + comPrt.Replace("'", "''") +
                              "','" + baudRate.Replace("'", "''") + "','" + timeOut.Replace("'", "''") + 
                              "','" + ftpHomeDir.Replace("'", "''") + "', '" + smtpIP.Replace("'", "''") +
                              "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void asgnPrvlgToRole(int prvldg_id, int role_id,
      string in_strDte, string in_endDte)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string endDate = "4000-12-31 23:59:59";
            if (in_strDte.Length < 19)
            {
                in_strDte = dateStr;
            }
            else
            {
                in_strDte = DateTime.ParseExact(
            in_strDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (in_endDte.Length < 19)
            {
                in_endDte = endDate;
            }
            else
            {
                in_endDte = DateTime.ParseExact(
            in_endDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sqlStr = "INSERT INTO sec.sec_roles_n_prvldgs(role_id, prvldg_id, valid_start_date, valid_end_date, created_by, " +
                              "creation_date, last_update_by, last_update_date) VALUES (" + role_id + ", " + prvldg_id + ", '" +
                              in_strDte + "', '" + in_endDte + "', " + Global.mySecurity.user_id + ", '" + dateStr +
                              "', " + Global.mySecurity.user_id + ", '" + dateStr + "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void asgnMdlToPlcy(int plcy_id, int mdl_id, bool nwvalue, string nwactions)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_audit_trail_tbls_to_enbl(policy_id, module_id, action_typs_to_track, enable_tracking, created_by, " +
                              "creation_date, last_update_by, last_update_date) VALUES (" + plcy_id + ", " + mdl_id +
                              ", '" + nwactions.Replace("'", "''") + "', " + nwvalue + ", " + Global.mySecurity.user_id + ", '" + dateStr +
                              "', " + Global.mySecurity.user_id + ", '" + dateStr + "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void storeOldPassword(Int64 usrid, string encrptdpswd)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "INSERT INTO sec.sec_users_old_pswds(user_id, old_password, date_added) " +
              "VALUES (" + usrid + ", '" + encrptdpswd.Replace("'", "''") +
              "', '" + dateStr + "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }
        #endregion

        #region "UPDATE STATEMENTS..."
        public static void enblDsblAllwdExtraInfos(long combntnID,
       bool isEnbld)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_allwd_other_infos " +
                     " SET is_enabled = '" +
             Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(isEnbld) + "', last_update_by = " + Global.mySecurity.user_id +
             ", last_update_date = '" + dateStr + "' " +
             "WHERE ((comb_info_id = " + combntnID + "))";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void unlockUsrAccnt(string username)
        {
            //Set failed_login_atmpts in sec.sec_users to 0
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_users SET failed_login_atmpts = 0, last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' WHERE (lower(user_name) = '" +
              username.Replace("'", "''").ToLower() + "')";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void unsuspendAccnt(string username)
        {
            //Unsuspends a user's account
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_users SET is_suspended = FALSE, last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' WHERE (lower(user_name) = '" +
              username.Replace("'", "''").ToLower() + "')";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void suspendAccnt(string username)
        {
            //suspends a user's account
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_users SET is_suspended = TRUE, last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr +
              "' WHERE (lower(user_name) = '" +
              username.Replace("'", "''").ToLower() + "')";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void changeUsrVldStrDate(string username, string inpt_date)
        {
            //Changes a user's account's valid start date
            inpt_date = DateTime.ParseExact(
         inpt_date, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");

            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_users SET valid_start_date = '" + inpt_date + "', last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' WHERE (lower(user_name) = '" +
              username.Replace("'", "''").ToLower() + "')";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void changeUsrVldEndDate(string username, string inpt_date)
        {
            //Changes a user's account's valid start date
            inpt_date = DateTime.ParseExact(
         inpt_date, "dd-MMM-yyyy HH:mm:ss",
         System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_users SET valid_end_date = '" + inpt_date + "', last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' WHERE (lower(user_name) = '" +
              username.Replace("'", "''").ToLower() + "')";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void changeUserPswd(Int64 usrid, string pswd)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_users SET usr_password = md5('" + Global.myNwMainFrm.cmmnCode.encrypt(pswd, CommonCode.CommonCodes.AppKey).Replace("'", "''") +
            "'), failed_login_atmpts = 0, last_pswd_chng_time = '" + dateStr + "', is_pswd_temp = TRUE, last_update_by = " +
            Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' WHERE (user_id = '" + usrid.ToString() + "')";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        /*public static void updateUser(string username, long ownrID,
          string in_strDte, string in_endDte, long cstmrID, string modulesNeeded, string usrCode4Trns)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string endDate = "4000-12-31 23:59:59";
            if (in_strDte.Length < 19)
            {
                in_strDte = dateStr;
            }
            else
            {
                in_strDte = DateTime.ParseExact(
            in_strDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (in_endDte.Length < 19)
            {
                in_endDte = endDate;
            }
            else
            {
                in_endDte = DateTime.ParseExact(
            in_endDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sqlStr = "UPDATE sec.sec_users SET person_id = " + ownrID + ", customer_id = " + cstmrID + ", valid_start_date = '" +
              in_strDte + "', valid_end_date = '" + in_endDte + "', last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "', modules_needed = '" + modulesNeeded.Replace("'", "''") +
              "', code_for_trns_nums = '" + usrCode4Trns.Replace("'", "''") + "' WHERE(lower(user_name) = '" + username.Replace("'", "''").ToLower() + "')";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }*/

        public static void updateUser(long usID, string username, long ownrID,
          string in_strDte, string in_endDte, long cstmrID, string modulesNeeded, string usrCode4Trns)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string endDate = "4000-12-31 23:59:59";
            if (in_strDte.Length < 19)
            {
                in_strDte = dateStr;
            }
            else
            {
                in_strDte = DateTime.ParseExact(
            in_strDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (in_endDte.Length < 19)
            {
                in_endDte = endDate;
            }
            else
            {
                in_endDte = DateTime.ParseExact(
            in_endDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sqlStr = "UPDATE sec.sec_users SET person_id = " + ownrID + 
                ", customer_id = " + cstmrID + 
                ", valid_start_date = '" +  in_strDte + 
              "', valid_end_date = '" + in_endDte + 
              "', last_update_by = " + Global.mySecurity.user_id + 
              ", last_update_date = '" + dateStr + 
              "', modules_needed = '" + modulesNeeded.Replace("'", "''") +
              "', code_for_trns_nums = '" + usrCode4Trns.Replace("'", "''") +
              "', user_name = '" + username.Replace("'", "''") +
              "' WHERE(user_id = " + usID + ")";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void updateUsersPrticulrRole(long usrID, int roleID,
      string in_strDte, string in_endDte)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string endDate = "4000-12-31 23:59:59";
            if (in_strDte.Length < 19)
            {
                in_strDte = dateStr;
            }
            else
            {
                in_strDte = DateTime.ParseExact(
            in_strDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (in_endDte.Length < 19)
            {
                in_endDte = endDate;
            }
            else
            {
                in_endDte = DateTime.ParseExact(
            in_endDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sqlStr = "UPDATE sec.sec_users_n_roles SET valid_start_date = '" +
              in_strDte + "', valid_end_date = '" + in_endDte + "', last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' " +
              "WHERE((user_id = " + usrID + ") AND (role_id = " + roleID + "))";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void updateRolesPrticulrPrvldg(int prvldgID, int roleID,
      string in_strDte, string in_endDte)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string endDate = "4000-12-31 23:59:59";
            if (in_strDte.Length < 19)
            {
                in_strDte = dateStr;
            }
            else
            {
                in_strDte = DateTime.ParseExact(
            in_strDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (in_endDte.Length < 19)
            {
                in_endDte = endDate;
            }
            else
            {
                in_endDte = DateTime.ParseExact(
            in_endDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sqlStr = "UPDATE sec.sec_roles_n_prvldgs SET valid_start_date = '" +
              in_strDte + "', valid_end_date = '" + in_endDte + "', last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' " +
              "WHERE((prvldg_id = " + prvldgID + ") AND (role_id = " + roleID + "))";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void updateRole(int roleID, string rolename,
      string in_strDte, string in_endDte, bool canAssign)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string endDate = "4000-12-31 23:59:59";
            if (in_strDte.Length < 19)
            {
                in_strDte = dateStr;
            }
            else
            {
                in_strDte = DateTime.ParseExact(
            in_strDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (in_endDte.Length < 19)
            {
                in_endDte = endDate;
            }
            else
            {
                in_endDte = DateTime.ParseExact(
            in_endDte, "dd-MMM-yyyy HH:mm:ss",
            System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sqlStr = "UPDATE sec.sec_roles SET role_name = '" + rolename.Replace("'", "''") +
              "', valid_start_date = '" + in_strDte + "', valid_end_date = '" + in_endDte + "', last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "', can_mini_admins_assign = '"
              + Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(canAssign) + "' " +
              "WHERE(role_id = " + roleID + ")";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void undefaultAllPlcys()
        {
            //Set is_default to false for all policies where it is true
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_security_policies SET is_default = FALSE, last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' WHERE (is_default = TRUE)";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void updatePlcy(int plcyID, string plcy_nm, int faild_lgns
          , int expry_days, int auto_unlck, bool rqr_caps, bool rqr_smll, bool rqr_dgts, bool rqr_wild
          , string rgrmnt_cmdntns, bool is_dflt, int old_pwd_cnt, int min_pwd_len, int max_pwd_len
          , int max_no_recs, bool allw_rptn, bool allw_unm, decimal sessn_tme)
        {
            //Set is_default to false for all policies where it is true
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_security_policies " +
            "SET policy_name='" + plcy_nm.Replace("'", "''") + "', max_failed_lgn_attmpts=" +
            faild_lgns + ", pswd_expiry_days=" + expry_days + ", " +
                    "auto_unlocking_time_mins=" + auto_unlck + ", pswd_require_caps=" + rqr_caps + ", pswd_require_small=" + rqr_smll + ", " +
                    "pswd_require_dgt=" + rqr_dgts + ", pswd_require_wild=" + rqr_wild + ", pswd_reqrmnt_combntns='" + rgrmnt_cmdntns + "', " +
                    "is_default=" + is_dflt + ", last_update_by=" + Global.mySecurity.user_id + ", " +
                    "last_update_date='" + dateStr + "', old_pswd_cnt_to_disallow=" + old_pwd_cnt + ", pswd_min_length=" + min_pwd_len + ", " +
                    "pswd_max_length=" + max_pwd_len + ", max_no_recs_to_dsply=" + max_no_recs + ", allow_repeating_chars=" + allw_rptn + ", " +
                    "allow_usrname_in_pswds=" + allw_unm + ", session_timeout =" + sessn_tme +
         " WHERE (policy_id = " + plcyID + ")";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void enbldisableTracking(int plcyID, int mdlID, bool nwvalue)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_audit_trail_tbls_to_enbl SET enable_tracking = " +
              nwvalue + ", last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' " +
              "WHERE((policy_id = " + plcyID + ") AND (module_id = " + mdlID + "))";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void updateActnsToTrack(int plcyID, int mdlID, bool nwvalue, string nwactions)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_audit_trail_tbls_to_enbl SET enable_tracking = " +
              nwvalue + ", action_typs_to_track = '" + nwactions.Replace("'", "''") + "', last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' " +
              "WHERE((policy_id = " + plcyID + ") AND (module_id = " + mdlID + "))";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void undefaultAllEmlSvrs()
        {
            //Set is_default to false for all Email Servers where it is true
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_email_servers SET is_default = FALSE, last_update_by = " +
              Global.mySecurity.user_id + ", last_update_date = '" + dateStr + "' WHERE (is_default = TRUE)";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void updateEmlSvrs(int svrID, string smtp_nm, string mail_unme
          , string mail_pwd, int port_no, bool is_dflt, string actv_drctry,
          string ftpserver, string ftpunm, string ftppswd, int ftpport, string ftpdir, bool enfc
          , string pgdir, string bckpdir, string comPrt, string baudRate, string timeOut, string ftpHomeDir, string smtpIP)
        {
            //Update Server Info
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE sec.sec_email_servers " +
            "SET smtp_client='" + smtp_nm.Replace("'", "''") + "', mail_user_name='" +
            mail_unme.Replace("'", "''") + "', mail_password='" + Global.myNwMainFrm.cmmnCode.encrypt1(mail_pwd, CommonCode.CommonCodes.AppKey).Replace("'", "''") +
                    "', smtp_port=" + port_no + ", is_default=" + is_dflt + ", actv_drctry_domain_name = '" + actv_drctry.Replace("'", "''") +
                    "', last_update_by=" + Global.mySecurity.user_id + ", " +
                    "last_update_date='" + dateStr +
                    "', ftp_server_url='" + ftpserver.Replace("'", "''") + "', ftp_user_name='" +
            ftpunm.Replace("'", "''") + "', ftp_user_pswd='" + Global.myNwMainFrm.cmmnCode.encrypt1(ftppswd, CommonCode.CommonCodes.AppKey).Replace("'", "''") +
                    "', ftp_port=" + ftpport + ", ftp_app_sub_directory = '" + ftpdir.Replace("'", "''") +
                    "', enforce_ftp = '" + Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(enfc) +
                    "', pg_dump_dir = '" + pgdir.Replace("'", "''") +
                              "', backup_dir = '" + bckpdir.Replace("'", "''") + "', com_port = '" + comPrt.Replace("'", "''") +
                              "', baud_rate = '" + baudRate.Replace("'", "''") + "', timeout = '" + timeOut.Replace("'", "''") +
                              "', ftp_user_start_directory = '" + ftpHomeDir.Replace("'", "''") +
                              "', inhouse_smtp_ip = '" + smtpIP.Replace("'", "''") + "' WHERE (server_id = " + svrID + ")";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        #endregion

        #region "DELETE STATEMENTS..."
        public static void deleteAllwdExtraInfos(long combntnID)
        {
            //string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "DELETE FROM sec.sec_allwd_other_infos " +
             "WHERE ((comb_info_id = " + combntnID + "))";
            Global.myNwMainFrm.cmmnCode.deleteDataNoParams(sqlStr);
        }

        public static bool hasUsrEvrLgdIn(long usrID)
        {
            //string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "SELECT count(1) from sec.sec_track_user_logins " +
             "WHERE ((user_id = " + usrID + "))";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            try
            {
                if (dtst.Tables[0].Rows.Count > 0)
                {
                    if (long.Parse(dtst.Tables[0].Rows[0][0].ToString()) > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        public static void deleteUser(long usrID, string usrNm)
        {
            Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "Deleting User:" + usrNm;
            string sqlStr = "DELETE FROM sec.sec_users_n_roles " +
             "WHERE ((user_id = " + usrID + "))";
            Global.myNwMainFrm.cmmnCode.deleteDataNoParams(sqlStr);

            sqlStr = "DELETE FROM sec.sec_users " +
             "WHERE ((user_id = " + usrID + "))";
            Global.myNwMainFrm.cmmnCode.deleteDataNoParams(sqlStr);

            Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
        }
        #endregion

        #region "SELECT STATEMENTS..."
        #region "USER PANEL..."
        public static DataSet get_Basic_UserInfo(string searchWord, string searchIn,
          Int64 offset, int limit_size)
        {
            string strSql = "";
            if (searchIn == "Owned By")
            {
                strSql = @"SELECT a.user_name, 
        CASE WHEN a.person_id>0 THEN trim(b.title || ' ' || b.sur_name || ', ' || b.first_name 
        || ' ' || b.other_names) ELSE scm.get_cstmr_splr_name(a.customer_id) END fullname, 
        a.user_id, b.person_id, a.customer_id, a.modules_needed, a.code_for_trns_nums FROM sec.sec_users a LEFT OUTER JOIN prs.prsn_names_nos b 
    ON (a.person_id = b.person_id) WHERE ((CASE WHEN a.person_id>0 THEN trim(b.title || ' ' || b.sur_name || ', ' || b.first_name 
        || ' ' || b.other_names) ELSE scm.get_cstmr_splr_name(a.customer_id) END) ilike '" + searchWord.Replace("'", "''") + "') ORDER BY a.user_id LIMIT " + limit_size +
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "User Name")
            {
                strSql = @"SELECT a.user_name, CASE WHEN a.person_id>0 THEN trim(b.title || ' ' || b.sur_name || ', ' || b.first_name 
        || ' ' || b.other_names) ELSE scm.get_cstmr_splr_name(a.customer_id) END fullname, 
        a.user_id, b.person_id, a.customer_id, a.modules_needed, a.code_for_trns_nums
         FROM sec.sec_users a 
         LEFT OUTER JOIN prs.prsn_names_nos b ON (a.person_id = b.person_id) WHERE (a.user_name ilike '" +
                  searchWord.Replace("'", "''") + "') ORDER BY a.user_id LIMIT " +
                  limit_size + " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Role Name")
            {
                strSql = @"SELECT distinct a.user_name, CASE WHEN a.person_id>0 THEN trim(b.title || ' ' || b.sur_name || ', ' || b.first_name 
        || ' ' || b.other_names) ELSE scm.get_cstmr_splr_name(a.customer_id) END fullname, 
        a.user_id, b.person_id, a.customer_id, a.modules_needed, a.code_for_trns_nums
        FROM ((sec.sec_users a " +
                  "LEFT OUTER JOIN prs.prsn_names_nos b ON (a.person_id = b.person_id)) LEFT OUTER JOIN " +
                  "sec.sec_users_n_roles c ON a.user_id = c.user_id) LEFT OUTER JOIN sec.sec_roles d " +
                  "ON d.role_id = c.role_id WHERE (d.role_name ilike '" +
                  searchWord.Replace("'", "''") + "') ORDER BY a.user_id LIMIT " +
                  limit_size + " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            Global.myNwMainFrm.usrs_SQL = strSql;
            return dtst;
        }

        public static Int64 get_total_Users(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "Owned By")
            {
                strSql = "select count(user_id) FROM sec.sec_users a LEFT OUTER JOIN prs.prsn_names_nos b " +
            @"ON (a.person_id = b.person_id) WHERE ((CASE WHEN a.person_id>0 THEN trim(b.title || ' ' || b.sur_name || ', ' || b.first_name 
        || ' ' || b.other_names) ELSE scm.get_cstmr_splr_name(a.customer_id) END) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "User Name")
            {
                strSql = "select count(user_id) FROM sec.sec_users a LEFT OUTER JOIN prs.prsn_names_nos b " +
            "ON (a.person_id = b.person_id) WHERE (a.user_name ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "Role Name")
            {
                strSql = "SELECT count(distinct a.user_id) FROM ((sec.sec_users a " +
                  "LEFT OUTER JOIN prs.prsn_names_nos b ON (a.person_id = b.person_id)) LEFT OUTER JOIN " +
                  "sec.sec_users_n_roles c ON a.user_id = c.user_id) LEFT OUTER JOIN sec.sec_roles d " +
                  "ON d.role_id = c.role_id WHERE (d.role_name ilike '" +
                  searchWord.Replace("'", "''") + "')";
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static string get_Users_Rec_Hstry(string username)
        {
            string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
              "FROM sec.sec_users a WHERE(lower(a.user_name) = '" + username.Replace("'", "''").ToLower() + "')";
            string fnl_str = "";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                  "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                  Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                  "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        public static string get_Usr_Roles_Rec_Hstry(long usrID, int roleID)
        {
            string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD'),'DD-Mon-YYYY'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD'),'DD-Mon-YYYY') " +
            "FROM sec.sec_users_n_roles a WHERE(a.user_id = " + usrID + ") AND (a.role_id = " + roleID + ")";
            string fnl_str = "";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                  "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                  Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                  "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        public static DataSet get_AccountInfo(string username)
        {
            string strSql = @"SELECT failed_login_atmpts, 
to_char(to_timestamp(last_login_atmpt_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
to_char(to_timestamp(last_pswd_chng_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
        to_char(to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
to_char(to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
(extract('years' from age(now(), to_timestamp(last_pswd_chng_time, 'YYYY-MM-DD HH24:MI:SS'))) || ' yr(s) '" +
              "|| extract('months' from age(now(), to_timestamp(last_pswd_chng_time, 'YYYY-MM-DD HH24:MI:SS'))) || ' mon(s) '" +
              "|| extract('days' from age(now(), to_timestamp(last_pswd_chng_time, 'YYYY-MM-DD HH24:MI:SS'))) || ' day(s) ') pwdage" +
              " FROM sec.sec_users WHERE (lower(user_name) = '" + username.Replace("'", "''").ToLower() + "')";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static string get_user_name(Int64 userID)
        {
            //Gets the User Name 
            string sqlStr = "SELECT user_name FROM " +
            "sec.sec_users WHERE user_id = " + userID + "";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string getPrsnName(long prsnID)
        {
            //Gets the Person Name 
            string sqlStr = "SELECT trim(title || ' ' || sur_name || ', ' || first_name || ' ' || other_names) fullname FROM " +
            "prs.prsn_names_nos WHERE person_id = " + prsnID + "";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static DataSet get_Persons(string searchWord, string searchIn,
          Int64 offset, int limit_size, long prsnID)
        {

            string strSql = "";
            if (prsnID > 0)
            {
                strSql = "SELECT a.person_id, a.local_id_no " +
                  ", trim(a.title || ' ' || a.sur_name || ', ' || a.first_name " +
                  "|| ' ' || a.other_names) fullname FROM prs.prsn_names_nos a " +
             "WHERE (a.person_id = " + prsnID.ToString() + ") ORDER BY a.person_id LIMIT " + limit_size +
             " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Full Name")
            {
                strSql = "SELECT a.person_id, a.local_id_no " +
                  ", trim(a.title || ' ' || a.sur_name || ', ' || a.first_name " +
                  "|| ' ' || a.other_names) fullname FROM prs.prsn_names_nos a " +
                  "WHERE (concat(a.sur_name, ', ', a.first_name, ' ', " +
            "a.other_names) ilike '" + searchWord.Replace("'", "''") + "') ORDER BY a.person_id LIMIT " + limit_size +
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "ID No.")
            {
                strSql = "SELECT a.person_id, a.local_id_no " +
                  ", trim(a.title || ' ' || a.sur_name || ', ' || a.first_name " +
                  "|| ' ' || a.other_names) fullname FROM prs.prsn_names_nos a " +
                  "WHERE (a.local_id_no ilike '" + searchWord.Replace("'", "''") + "') ORDER BY a.person_id LIMIT " + limit_size +
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            Global.myNwMainFrm.prsns_SQL = strSql;
            return dtst;
        }

        public static Int64 get_total_Prsns(string searchWord, string searchIn, long prsnID)
        {
            string strSql = "";
            if (prsnID > 0)
            {
                strSql = "SELECT count(1) FROM prs.prsn_names_nos a " +
                  "WHERE (a.person_id = " + prsnID.ToString() + ")";
            }
            else if (searchIn == "Full Name")
            {
                strSql = "SELECT count(1) FROM prs.prsn_names_nos a " +
                  "WHERE (concat(a.sur_name, ', ', a.first_name, ' ', " +
            "a.other_names) ilike '" + searchWord.Replace("'", "''") + "')";
            }
            else if (searchIn == "ID No.")
            {
                strSql = "SELECT count(1) FROM prs.prsn_names_nos a " +
                  "WHERE (a.local_id_no ilike '" + searchWord.Replace("'", "''") + "')";
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static DataSet get_Roles(string searchWord, string searchIn,
      Int64 offset, int limit_size)
        {
            string strSql = "";
            if (searchIn == "Role Name")
            {
                strSql = "SELECT role_id, role_name FROM sec.sec_roles WHERE ((now() between to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
                      "to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS')) AND (role_name ilike '" + searchWord.Replace("'", "''") + "')) ORDER BY role_name LIMIT " + limit_size +
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static Int64 get_total_Roles(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "Role Name")
            {
                strSql = "SELECT count(role_id) FROM sec.sec_roles WHERE ((now() between to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
                      "to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS')) AND (role_name ilike '" + searchWord.Replace("'", "''") + "'))";
            }

            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static DataSet get_Users_Roles(long usrID)
        {
            //Gets the Roles a user has 
            string sqlStr = @"SELECT b.role_name, 
to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
a.role_id " +
         "FROM sec.sec_users_n_roles a, sec.sec_roles b WHERE ((a.role_id = b.role_id) AND (a.user_id = " + usrID + ")) ORDER BY 1";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            Global.myNwMainFrm.usr_roles_SQL = sqlStr;
            return dtSt;
        }

        public static DataSet get_Users_Particular_Role(long usrID, int roleID)
        {
            //Gets the a particular Roles a user has selected 
            string sqlStr = @"SELECT to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')  " +
         "FROM sec.sec_users_n_roles a WHERE ((a.role_id = " + roleID + ") AND (a.user_id = " + usrID + "))";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            return dtSt;
        }

        #endregion

        #region "GENERAL ..."
        public static string getModuleAdtTbl(string mdl_name)
        {
            //Example module name 'Security'
            DataSet dtSt = new DataSet();
            string sqlStr = "select audit_trail_tbl_name from sec.sec_modules where module_name = '" +
              mdl_name.Replace("'", "''") + "'";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region "ROLES PANEL..."
        public static DataSet get_Roles_Excel(string searchWord, string searchIn,
     Int64 offset, int limit_size)
        {
            string strSql = "";
            strSql = @"SELECT distinct a.role_id, a.role_name, 
to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'),
CASE WHEN can_mini_admins_assign='1' THEN 'YES' ELSE 'NO' END canAssign ,
c.prvldg_name, d.module_name,
to_char(to_timestamp(b.valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, to_char(to_timestamp(b.valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') 
FROM ((sec.sec_roles a " +
                  "LEFT OUTER JOIN sec.sec_roles_n_prvldgs b ON a.role_id = b.role_id) LEFT OUTER JOIN sec.sec_prvldgs c ON " +
                  "b.prvldg_id = c.prvldg_id) LEFT OUTER JOIN sec.sec_modules d ON c.module_id = d.module_id " +
                  "WHERE (1=1) ORDER BY d.module_name, a.role_name, c.prvldg_name LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            // Global.myNwMainFrm.roles_SQL = strSql;
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static DataSet get_Roles_Main(string searchWord, string searchIn,
      Int64 offset, int limit_size)
        {
            string strSql = "";
            if (searchIn == "Role Name")
            {
                strSql = @"SELECT role_id, role_name, 
        to_char(to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, to_char(to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'),
CASE WHEN can_mini_admins_assign='1' THEN 'YES' ELSE 'NO' END canAssign 
FROM sec.sec_roles " +
                  "WHERE ((role_name ilike '" + searchWord.Replace("'", "''") +
                  "')) ORDER BY role_name LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Priviledge Name")
            {
                strSql = @"SELECT distinct a.role_id, a.role_name, 
to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
FROM (sec.sec_roles a " +
                  "LEFT OUTER JOIN sec.sec_roles_n_prvldgs b ON a.role_id = b.role_id) LEFT OUTER JOIN sec.sec_prvldgs c ON b.prvldg_id = c.prvldg_id " +
                  "WHERE ((c.prvldg_name ilike '" + searchWord.Replace("'", "''") +
                  "')) ORDER BY a.role_name LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Owner Module")
            {
                strSql = @"SELECT distinct a.role_id, a.role_name, 
to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') 
FROM ((sec.sec_roles a " +
                  "LEFT OUTER JOIN sec.sec_roles_n_prvldgs b ON a.role_id = b.role_id) LEFT OUTER JOIN sec.sec_prvldgs c ON " +
                  "b.prvldg_id = c.prvldg_id) LEFT OUTER JOIN sec.sec_modules d ON c.module_id = d.module_id " +
                  "WHERE ((d.module_name ilike '" + searchWord.Replace("'", "''") +
                  "')) ORDER BY a.role_name LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.myNwMainFrm.roles_SQL = strSql;
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static Int64 get_total_Roles_Main(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "Role Name")
            {
                strSql = "SELECT count(role_id) FROM sec.sec_roles " +
                  "WHERE ((role_name ilike '" + searchWord.Replace("'", "''") +
                  "'))";
            }
            else if (searchIn == "Priviledge Name")
            {
                strSql = "SELECT count(distinct a.role_id) FROM (sec.sec_roles a " +
                  "LEFT OUTER JOIN sec.sec_roles_n_prvldgs b ON a.role_id = b.role_id) LEFT OUTER JOIN sec.sec_prvldgs c ON b.prvldg_id = c.prvldg_id " +
                  "WHERE ((c.prvldg_name ilike '" + searchWord.Replace("'", "''") +
                  "'))";
            }
            else if (searchIn == "Owner Module")
            {
                strSql = "SELECT count(distinct a.role_id) FROM ((sec.sec_roles a " +
                  "LEFT OUTER JOIN sec.sec_roles_n_prvldgs b ON a.role_id = b.role_id) LEFT OUTER JOIN sec.sec_prvldgs c ON " +
                  "b.prvldg_id = c.prvldg_id) LEFT OUTER JOIN sec.sec_modules d ON c.module_id = d.module_id " +
                  "WHERE ((d.module_name ilike '" + searchWord.Replace("'", "''") +
                  "'))";
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static DataSet get_Roles_Prvldgs(int roleID)
        {
            //Gets the  Priviledges a Roles has 
            string sqlStr = @"SELECT b.prvldg_name, c.module_name, 
      to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, b.prvldg_id, c.module_id " +
            "FROM sec.sec_roles_n_prvldgs a, sec.sec_prvldgs b, sec.sec_modules c WHERE " +
            "((a.prvldg_id = b.prvldg_id) AND (b.module_id = c.module_id) AND (a.role_id = " + roleID + "))";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            Global.myNwMainFrm.role_prvldgs_SQL = sqlStr;
            return dtSt;
        }

        public static DataSet get_prvldgs(string searchWord, string searchIn,
        Int64 offset, int limit_size)
        {
            string strSql = "";
            if (searchIn == "Priviledge Name")
            {
                strSql = "SELECT b.prvldg_name, c.module_name, b.prvldg_id, c.module_id " +
              "FROM sec.sec_prvldgs b, sec.sec_modules c WHERE " +
              "((b.module_id = c.module_id) AND (b.prvldg_name ilike '" + searchWord.Replace("'", "''") +
                  "')) ORDER BY c.module_name, b.prvldg_name LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Owner Module")
            {
                strSql = "SELECT b.prvldg_name, c.module_name, b.prvldg_id, c.module_id " +
              "FROM sec.sec_prvldgs b, sec.sec_modules c WHERE " +
              "((b.module_id = c.module_id) AND (c.module_name ilike '" + searchWord.Replace("'", "''") +
                  "')) ORDER BY c.module_name, b.prvldg_name LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static Int64 get_total_prvldgs(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "Priviledge Name")
            {
                strSql = "SELECT count(b.prvldg_id) " +
              "FROM sec.sec_prvldgs b, sec.sec_modules c WHERE " +
              "((b.module_id = c.module_id) AND (b.prvldg_name ilike '" + searchWord.Replace("'", "''") +
                  "'))";
            }
            else if (searchIn == "Owner Module")
            {
                strSql = "SELECT count(b.prvldg_id) " +
              "FROM sec.sec_prvldgs b, sec.sec_modules c WHERE " +
              "((b.module_id = c.module_id) AND (c.module_name ilike '" + searchWord.Replace("'", "''") +
                  "'))";
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static DataSet get_Roles_Prtclr_Prvldg(int roleID, int prvldgID)
        {
            //Gets a particular priviledge of a given role
            string sqlStr = @"SELECT 
      to_char(to_timestamp(a.valid_start_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
to_char(to_timestamp(a.valid_end_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')  " +
         "FROM sec.sec_roles_n_prvldgs a WHERE ((a.role_id = " + roleID +
         ") AND (a.prvldg_id = " + prvldgID + "))";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            return dtSt;
        }

        public static string get_Roles_Rec_Hstry(int roleID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
              "FROM sec.sec_roles a WHERE(role_id = " + roleID + ")";
            string fnl_str = "";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                  "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                  Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                  "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        public static string get_Roles_Prvlg_Rec_Hstry(int prvldgID, int roleID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM sec.sec_roles_n_prvldgs a WHERE(a.prvldg_id = " + prvldgID + ") AND (a.role_id = " + roleID + ")";
            string fnl_str = "";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                  "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                  Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                  "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region "MODULES PANEL..."
        public static DataSet get_Mdls_Main(string searchWord, string searchIn,
      Int64 offset, int limit_size)
        {
            string strSql = "";
            if (searchIn == "Module Name")
            {
                strSql = @"SELECT module_id, module_name, module_desc, 
        to_char(to_timestamp(date_added,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, audit_trail_tbl_name FROM sec.sec_modules " +
                  "WHERE ((module_name ilike '" + searchWord.Replace("'", "''") +
                  "')) ORDER BY module_name LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Priviledge Name")
            {
                strSql = @"SELECT distinct a.module_id, a.module_name, a.module_desc, 
        to_char(to_timestamp(a.date_added,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, a.audit_trail_tbl_name FROM (sec.sec_modules a " +
                  "LEFT OUTER JOIN sec.sec_prvldgs b ON a.module_id = b.module_id)" +
                  "WHERE ((b.prvldg_name ilike '" + searchWord.Replace("'", "''") +
                  "')) ORDER BY a.module_name LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Subgroup Name")
            {
                strSql = @"SELECT distinct a.module_id, a.module_name, a.module_desc, 
        to_char(to_timestamp(a.date_added,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
      , a.audit_trail_tbl_name FROM (sec.sec_modules a " +
             "LEFT OUTER JOIN sec.sec_module_sub_groups b ON a.module_id = b.module_id)" +
             "WHERE ((b.sub_group_name ilike '" + searchWord.Replace("'", "''") +
             "'))";
            }
            Global.myNwMainFrm.mdls_SQL = strSql;
            Global.myNwMainFrm.extinf_mdls_SQL = strSql;
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static Int64 get_total_Mdls_Main(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "Module Name")
            {
                strSql = "SELECT count(module_id) FROM sec.sec_modules " +
                  "WHERE ((module_name ilike '" + searchWord.Replace("'", "''") +
                  "'))";
            }
            else if (searchIn == "Priviledge Name")
            {
                strSql = "SELECT count(distinct a.module_id) FROM (sec.sec_modules a " +
                  "LEFT OUTER JOIN sec.sec_prvldgs b ON a.module_id = b.module_id)" +
                  "WHERE ((b.prvldg_name ilike '" + searchWord.Replace("'", "''") +
                  "'))";
            }
            else if (searchIn == "Subgroup Name")
            {
                strSql = "SELECT count(distinct a.module_id) FROM (sec.sec_modules a " +
             "LEFT OUTER JOIN sec.sec_module_sub_groups b ON a.module_id = b.module_id)" +
             "WHERE ((b.sub_group_name ilike '" + searchWord.Replace("'", "''") +
             "'))";
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static DataSet get_Mdls_Prvldgs(int mdlID)
        {
            //Gets the  Priviledges a Module has 
            string sqlStr = "SELECT b.prvldg_name, b.prvldg_id, c.module_id " +
            "FROM sec.sec_prvldgs b, sec.sec_modules c WHERE " +
            "((b.module_id = c.module_id) AND (c.module_id = " + mdlID + "))";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            Global.myNwMainFrm.mdl_prvldgs_SQL = sqlStr;
            return dtSt;
        }

        #endregion

        #region "MODULES EXTRA INFO PANEL..."
        public static DataSet get_Mdls_SubGrps(int mdlID)
        {
            //Gets the  Subgroups a Module has 
            string sqlStr = "SELECT b.sub_group_name, b.main_table_name, " +
             @"b.row_pk_col_name, 
      to_char(to_timestamp(b.date_added,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
b.table_id, c.module_id " +
            "FROM sec.sec_module_sub_groups b, sec.sec_modules c WHERE " +
            "((b.module_id = c.module_id) AND (c.module_id = " + mdlID + "))";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            Global.myNwMainFrm.mdl_subgroups_SQL = sqlStr;
            return dtSt;
        }

        public static DataSet get_SubGrps_ExtInf_Labels(int tblID)
        {
            //Gets the  Extra Info Labels a Table has 
            string sqlStr = "SELECT b.pssbl_value, a.is_enabled, b.pssbl_value_id, " +
             "c.value_list_id, a.comb_info_id " +
            "FROM sec.sec_allwd_other_infos a, gst.gen_stp_lov_values b, gst.gen_stp_lov_names c WHERE " +
            @"((b.value_list_id = c.value_list_id) AND (b.pssbl_value_id = a.other_info_id) 
      AND (a.table_id = " + tblID + ")) ORDER BY 1";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            Global.myNwMainFrm.extinf_SQL = sqlStr;
            return dtSt;
        }

        public static string get_ExtInfo_Rec_Hstry(long combntnID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
             "FROM sec.sec_allwd_other_infos a WHERE(comb_info_id = " + combntnID + ")";
            string fnl_str = "";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                 "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                 Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                 "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region "POLICY PANEL..."
        public static DataSet get_Plcys(string searchWord, string searchIn,
      Int64 offset, int limit_size)
        {
            string strSql = "";
            if (searchIn == "Policy Name")
            {
                strSql = "SELECT policy_id, policy_name, max_failed_lgn_attmpts, pswd_expiry_days, " +
               "auto_unlocking_time_mins, pswd_require_caps, pswd_require_small, " +
               "pswd_require_dgt, pswd_require_wild, pswd_reqrmnt_combntns, is_default, " +
               "old_pswd_cnt_to_disallow, pswd_min_length, pswd_max_length, max_no_recs_to_dsply, " +
               "allow_repeating_chars, allow_usrname_in_pswds, session_timeout " +
                      "FROM sec.sec_security_policies " +
                      "WHERE ((policy_name ilike '" + searchWord.Replace("'", "''") +
                      "')) ORDER BY policy_name LIMIT " + limit_size +
                      " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.myNwMainFrm.plcys_SQL = strSql;
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static Int64 get_total_Plcy(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "Policy Name")
            {
                strSql = "SELECT count(policy_id) FROM sec.sec_security_policies " +
                  "WHERE ((policy_name ilike '" + searchWord.Replace("'", "''") +
                  "'))";
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static DataSet get_Plcy_Mdls(int plcyID)
        {
            string strSql = "SELECT a.module_id, a.module_name, a.audit_trail_tbl_name, " +
              "b.enable_tracking, b.action_typs_to_track FROM sec.sec_modules a LEFT OUTER JOIN " +
              "sec.sec_audit_trail_tbls_to_enbl b ON ((a.module_id = b.module_id) AND (b.policy_id = " +
              plcyID + ")) ORDER BY a.module_name";
            Global.myNwMainFrm.plcy_Adt_Tbls_SQL = strSql;
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static string[] get_Plcy_Prtclr_Mdl(int plcyID, int mdlID)
        {
            string strSql = "SELECT b.enable_tracking, b.action_typs_to_track FROM " +
              "sec.sec_audit_trail_tbls_to_enbl b WHERE((b.policy_id = " +
              plcyID + ") AND (b.module_id = " + mdlID + "))";
            Global.myNwMainFrm.plcy_Adt_Tbls_SQL = strSql;
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            string[] nwStr = new string[2];
            if (dtst.Tables[0].Rows.Count > 0)
            {
                nwStr[0] = dtst.Tables[0].Rows[0][0].ToString();
                nwStr[1] = dtst.Tables[0].Rows[0][1].ToString();
            }
            return nwStr;
        }

        public static string get_Plcys_Rec_Hstry(int plcyID)
        {
            string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
              "FROM sec.sec_security_policies a WHERE(policy_id = " + plcyID + ")";
            string fnl_str = "";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                  "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                  Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                  "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        public static string get_Plcy_Mdls_Rec_Hstry(int plcyID, int mdlID)
        {
            string strSQL = @"SELECT a.created_by, 
to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
            "FROM sec.sec_audit_trail_tbls_to_enbl a WHERE(a.policy_id = " + plcyID + ") AND (a.module_id = " + mdlID + ")";
            string fnl_str = "";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                  "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                  Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                  "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }

        #endregion

        #region "EMAIL SERVER PANEL..."
        public static DataSet get_Eml_Srvrs(string searchWord, string searchIn,
      Int64 offset, int limit_size)
        {
            string strSql = "";
            if (searchIn == "SMTP CLIENT")
            {
                strSql = "SELECT server_id, smtp_client, mail_user_name, mail_password, " +
                      "smtp_port, is_default, actv_drctry_domain_name, " +
                      "ftp_server_url, ftp_user_name, ftp_user_pswd, ftp_port, " +
                      "ftp_app_sub_directory, enforce_ftp, " +
                    "pg_dump_dir, backup_dir, com_port, baud_rate, timeout, ftp_user_start_directory, inhouse_smtp_ip " +
                      "FROM sec.sec_email_servers " +
                      "WHERE ((smtp_client ilike '" + searchWord.Replace("'", "''") +
                      "')) ORDER BY smtp_client LIMIT " + limit_size +
                      " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "SENDER's USER NAME")
            {
                strSql = "SELECT server_id, smtp_client, mail_user_name, mail_password, " +
              "smtp_port, is_default, actv_drctry_domain_name, " +
                                "ftp_server_url, ftp_user_name, ftp_user_pswd, ftp_port, " +
                                "ftp_app_sub_directory, enforce_ftp, " +
                    "pg_dump_dir, backup_dir, com_port, baud_rate, timeout, ftp_user_start_directory, inhouse_smtp_ip " +
              "FROM sec.sec_email_servers " +
              "WHERE ((mail_user_name ilike '" + searchWord.Replace("'", "''") +
              "')) ORDER BY mail_user_name LIMIT " + limit_size +
              " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            Global.myNwMainFrm.eml_srvs_SQL = strSql;
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static Int64 get_total_EmlSvr(string searchWord, string searchIn)
        {
            string strSql = "";
            if (searchIn == "SMTP CLIENT")
            {
                strSql = "SELECT count(server_id) " +
                      "FROM sec.sec_email_servers " +
                      "WHERE ((smtp_client ilike '" + searchWord.Replace("'", "''") +
                      "'))";
            }
            else if (searchIn == "SENDER's USER NAME")
            {
                strSql = "SELECT count(server_id) " +
              "FROM sec.sec_email_servers " +
              "WHERE ((mail_user_name ilike '" + searchWord.Replace("'", "''") +
              "'))";
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static string get_EmlSvr_Rec_Hstry(int svrID)
        {
            string strSQL = @"SELECT a.created_by, 
      to_char(to_timestamp(a.creation_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS'), 
      a.last_update_by, 
      to_char(to_timestamp(a.last_update_date,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY  HH24:MI:SS') " +
              "FROM sec.sec_email_servers a WHERE(server_id = " + svrID + ")";
            string fnl_str = "";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSQL);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                fnl_str = "CREATED BY: " + Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][0].ToString())) +
                  "\r\nCREATION DATE: " + dtst.Tables[0].Rows[0][1].ToString() + "\r\nLAST UPDATE BY:" +
                  Global.get_user_name(long.Parse(dtst.Tables[0].Rows[0][2].ToString())) +
                  "\r\nLAST UPDATE DATE: " + dtst.Tables[0].Rows[0][3].ToString();
                return fnl_str;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region "LOGINS PANEL..."
        public static DataSet get_Lgns(string searchWord, string searchIn,
      Int64 offset, int limit_size, bool shw_sccful, bool shw_faild)
        {
            string numFrmat = "999999999999999999999999999999";
            string strSql = "";
            string optional_str1 = "";
            string optional_str2 = "";
            if (shw_sccful == false || shw_faild == false)
            {
                if (shw_sccful == true)
                {
                    optional_str1 = " AND (a.was_lgn_atmpt_succsful = TRUE)";
                }
                else
                {
                    optional_str1 = " AND (a.was_lgn_atmpt_succsful = FALSE)";
                }
            }
            if (searchIn == "Login Number")
            {
                strSql = @"SELECT b.user_name, 
    to_char(to_timestamp(a.login_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
    CASE WHEN a.logout_time ='' THEN a.logout_time ELSE to_char(to_timestamp(a.logout_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END, 
    a.host_mach_details,  a.was_lgn_atmpt_succsful, a.user_id, a.login_number, 
    CASE WHEN a.last_active_time ='' THEN a.last_active_time ELSE to_char(to_timestamp(a.last_active_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END,
    lgn_remarks 
    FROM sec.sec_track_user_logins a 
    LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id 
    WHERE ((trim(to_char(a.login_number,'" + numFrmat + "')) ilike '" + searchWord.Replace("'", "''") + "')" + optional_str1 + ") ORDER BY a.login_number DESC LIMIT " + limit_size +
                  " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "User Name")
            {
                if (searchWord == "")
                {
                    optional_str2 = " OR (b.user_name IS NULL)";
                }
                strSql = @"SELECT b.user_name, 
    to_char(to_timestamp(a.login_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
    CASE WHEN a.logout_time ='' THEN a.logout_time ELSE to_char(to_timestamp(a.logout_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END, 
    a.host_mach_details,  a.was_lgn_atmpt_succsful, a.user_id, a.login_number, 
    CASE WHEN a.last_active_time ='' THEN a.last_active_time ELSE to_char(to_timestamp(a.last_active_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END,
    lgn_remarks 
    FROM sec.sec_track_user_logins a " +
            " LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
            "WHERE ((b.user_name ilike '" + searchWord.Replace("'", "''") +
            "')" + optional_str1 + "" + optional_str2 + ") ORDER BY a.login_number DESC LIMIT " + limit_size + //ORDER BY b.user_name ASC 
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Login Time")
            {
                strSql = @"SELECT b.user_name, 
    to_char(to_timestamp(a.login_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
    CASE WHEN a.logout_time ='' THEN a.logout_time ELSE to_char(to_timestamp(a.logout_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END, 
    a.host_mach_details,  a.was_lgn_atmpt_succsful, a.user_id, a.login_number, 
    CASE WHEN a.last_active_time ='' THEN a.last_active_time ELSE to_char(to_timestamp(a.last_active_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END,
    lgn_remarks 
    FROM sec.sec_track_user_logins a " +
            " LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
            "WHERE ((a.login_time ilike '" + searchWord.Replace("'", "''") +
            "')" + optional_str1 + ") ORDER BY a.login_number DESC LIMIT " + limit_size + //ORDER BY to_timestamp(a.login_time, 'YYYY-MM-DD HH24:MI:SS') DESC
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }
            else if (searchIn == "Logout Time")
            {
                if (searchWord == "")
                {
                    optional_str2 = " OR (a.logout_time IS NULL)";
                }
                strSql = @"SELECT b.user_name, 
    to_char(to_timestamp(a.login_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
    CASE WHEN a.logout_time ='' THEN a.logout_time ELSE to_char(to_timestamp(a.logout_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END, 
    a.host_mach_details,  a.was_lgn_atmpt_succsful, a.user_id, a.login_number, 
    CASE WHEN a.last_active_time ='' THEN a.last_active_time ELSE to_char(to_timestamp(a.last_active_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END,
    lgn_remarks 
    FROM sec.sec_track_user_logins a " +
            " LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
            "WHERE ((a.logout_time ilike '" + searchWord.Replace("'", "''") +
            "')" + optional_str1 + "" + optional_str2 + ") ORDER BY a.login_number DESC LIMIT " + limit_size +
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();
            }//ORDER BY a.logout_time ASC 
            else if (searchIn == "Machine Details")
            {
                strSql = @"SELECT b.user_name, 
    to_char(to_timestamp(a.login_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS'), 
    CASE WHEN a.logout_time ='' THEN a.logout_time ELSE to_char(to_timestamp(a.logout_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END, 
    a.host_mach_details,  a.was_lgn_atmpt_succsful, a.user_id, a.login_number, 
    CASE WHEN a.last_active_time ='' THEN a.last_active_time ELSE to_char(to_timestamp(a.last_active_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS') END,
    lgn_remarks 
    FROM sec.sec_track_user_logins a " +
            " LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
            "WHERE ((a.host_mach_details ilike '" + searchWord.Replace("'", "''") +
            "')" + optional_str1 + ") ORDER BY a.login_number DESC LIMIT " + limit_size +
            " OFFSET " + (Math.Abs(offset * limit_size)).ToString();//ORDER BY a.host_mach_details ASC
            }
            Global.myNwMainFrm.lgns_SQL = strSql;
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static Int64 get_total_lgns(string searchWord, string searchIn, bool shw_sccful, bool shw_faild)
        {
            string numFrmat = "999999999999999999999999999999";
            string strSql = "";
            string optional_str = "";
            string optional_str2 = "";
            if (shw_sccful == false || shw_faild == false)
            {
                if (shw_sccful == true)
                {
                    optional_str = " AND (a.was_lgn_atmpt_succsful = TRUE)";
                }
                else
                {
                    optional_str = " AND (a.was_lgn_atmpt_succsful = FALSE)";
                }
            }
            if (searchIn == "Login Number")
            {
                strSql = "SELECT count(a.login_number) FROM sec.sec_track_user_logins a " +
                  " LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
                  "WHERE ((trim(to_char(a.login_number,'" + numFrmat + "')) ilike '" + searchWord.Replace("'", "''") +
                  "')" + optional_str + ")";
            }
            else if (searchIn == "User Name")
            {
                if (searchWord == "")
                {
                    optional_str2 = " OR (b.user_name IS NULL)";
                }
                strSql = "SELECT count(a.login_number) FROM sec.sec_track_user_logins a " +
            " LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
            "WHERE ((b.user_name ilike '" + searchWord.Replace("'", "''") +
            "')" + optional_str + "" + optional_str2 + ")";
            }
            else if (searchIn == "Login Time")
            {
                strSql = "SELECT count(a.login_number) FROM sec.sec_track_user_logins a " +
            " LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
            "WHERE ((a.login_time ilike '" + searchWord.Replace("'", "''") +
            "')" + optional_str + ")";
            }
            else if (searchIn == "Logout Time")
            {
                if (searchWord == "")
                {
                    optional_str2 = " OR (a.logout_time IS NULL)";
                }
                strSql = "SELECT count(a.login_number) FROM sec.sec_track_user_logins a " +
            " LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
            "WHERE ((a.logout_time ilike '" + searchWord.Replace("'", "''") +
            "')" + optional_str + "" + optional_str2 + ")";
            }
            else if (searchIn == "Machine Details")
            {
                strSql = "SELECT count(a.login_number) FROM sec.sec_track_user_logins a " +
            " LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
            "WHERE ((a.host_mach_details ilike '" + searchWord.Replace("'", "''") +
            "')" + optional_str + ")";
            }
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region "AUDIT PANEL..."
        public static DataSet get_Adt_Trails(string searchWord, string searchIn,
      Int64 offset, int limit_size, string mdlname)
        {
            string numFrmat = "999999999999999999999999999999";
            string[] whereClsFrmts = { "trim(to_char(a.login_number,'" + numFrmat + "'))",
            "b.user_name", "a.action_details", "a.action_type",
        "to_char(to_timestamp(a.action_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')","c.host_mach_details"};
            string[] ordrByClsFrmts = { "a.login_number", "b.user_name", "a.action_details", "a.action_type", "a.action_time", "c.host_mach_details" };
            string[] sortOrder = { "DESC", "ASC", "ASC", "ASC", "ASC", "ASC" };
            int frmt_to_use = 0;
            string strSql = "";
            string optional_str = "";
            if (searchIn == "Login Number")
            {
                frmt_to_use = 0;
            }
            else if (searchIn == "User Name")
            {
                frmt_to_use = 1;
            }
            else if (searchIn == "Action Details")
            {
                frmt_to_use = 2;
            }
            else if (searchIn == "Action Type")
            {
                frmt_to_use = 3;
            }
            else if (searchIn == "Date/Time")
            {
                frmt_to_use = 4;
            }
            else if (searchIn == "Machine Used")
            {
                frmt_to_use = 5;
            }

            if (searchWord == "")
            {
                optional_str = " OR (" + ordrByClsFrmts[frmt_to_use] + " IS NULL)";
            }

            strSql = @"SELECT b.user_name, a.action_type, a.action_details, 
      to_char(to_timestamp(a.action_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')
, c.host_mach_details, " +
         "a.user_id, a.login_number FROM " + Global.getModuleAdtTbl(mdlname) + " a " +
         "LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
         "LEFT OUTER JOIN sec.sec_track_user_logins c ON a.login_number = c.login_number " +
         "WHERE ((" + whereClsFrmts[frmt_to_use] + " ilike '" + searchWord.Replace("'", "''") +
         "')" + optional_str + ") ORDER BY " + ordrByClsFrmts[frmt_to_use] + " " +
         sortOrder[frmt_to_use] + " LIMIT " + limit_size +
         " OFFSET " + (Math.Abs(offset * limit_size)).ToString();

            Global.myNwMainFrm.adt_SQL = strSql;
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }

        public static Int64 get_total_adt_trls(string searchWord, string searchIn, string mdlname)
        {
            string numFrmat = "999999999999999999999999999999";
            string[] whereClsFrmts = { "trim(to_char(a.login_number,'" + numFrmat + "'))",
            "b.user_name", "a.action_details", "a.action_type",
        "to_char(to_timestamp(a.action_time,'YYYY-MM-DD HH24:MI:SS'),'DD-Mon-YYYY HH24:MI:SS')","c.host_mach_details"};
            string[] ordrByClsFrmts = { "a.login_number", "b.user_name", "a.action_details",
        "a.action_type", "a.action_time", "c.host_mach_details" };
            string[] sortOrder = { "DESC", "ASC", "ASC", "ASC", "ASC", "ASC" };
            int frmt_to_use = 0;
            string strSql = "";
            string optional_str = "";
            if (searchIn == "Login Number")
            {
                frmt_to_use = 0;
            }
            else if (searchIn == "User Name")
            {
                frmt_to_use = 1;
            }
            else if (searchIn == "Action Details")
            {
                frmt_to_use = 2;
            }
            else if (searchIn == "Action Type")
            {
                frmt_to_use = 3;
            }
            else if (searchIn == "Date/Time")
            {
                frmt_to_use = 4;
            }
            else if (searchIn == "Machine Used")
            {
                frmt_to_use = 5;
            }

            if (searchWord == "")
            {
                optional_str = " OR (" + ordrByClsFrmts[frmt_to_use] + " IS NULL)";
            }

            strSql = "SELECT count(a.user_id) FROM " + Global.getModuleAdtTbl(mdlname) + " a " +
         "LEFT OUTER JOIN sec.sec_users b ON a.user_id = b.user_id " +
         "LEFT OUTER JOIN sec.sec_track_user_logins c ON a.login_number = c.login_number " +
         "WHERE ((" + whereClsFrmts[frmt_to_use] + " ilike '" + searchWord.Replace("'", "''") +
         "')" + optional_str + ")";

            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static DataSet get_Module_Nms()
        {
            string strSql = "SELECT module_name FROM sec.sec_modules Where module_id IN (select distinct b.module_id from sec.sec_audit_trail_tbls_to_enbl b)";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            return dtst;
        }
        #endregion
        #endregion

        #region "VERIFICATION STATEMENTS..."
        public static int getPlcyID(string plcy_name)
        {
            //Example policy name 'Standard ISO Password Policy'
            DataSet dtSt = new DataSet();
            string sqlStr = "select policy_id from sec.sec_security_policies where policy_name = '" +
              plcy_name.Replace("'", "''") + "'";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static int getEmlSvrID(string svr_name)
        {
            //Example server name 'smtp.gmail.com'
            DataSet dtSt = new DataSet();
            string sqlStr = "select server_id from sec.sec_email_servers where smtp_client = '" +
              svr_name.Replace("'", "''") + "'";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static bool doesUserHaveThisRole(string username, string rolename)
        {
            //Checks whether a given username 'admin' has a given user role
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT user_id FROM sec.sec_users_n_roles WHERE ((user_id = " +
                    Global.getUserID(username) + ") AND (role_id = " + Global.myNwMainFrm.cmmnCode.getRoleID(rolename) +
                    ") AND (now() between to_timestamp(valid_start_date,'YYYY-MM-DD HH24:MI:SS') AND " +
                    "to_timestamp(valid_end_date,'YYYY-MM-DD HH24:MI:SS')))";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool doesTableHvThsExtrInfoLbl(long tblID, int pssblVlID)
        {
            //Checks whether a given username 'admin' has a given user role
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT comb_info_id FROM sec.sec_allwd_other_infos WHERE ((table_id = " +
                tblID + ") AND (other_info_id = " + pssblVlID +
                "))";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool doesUserHaveThisRole_Display(string username, string rolename)
        {
            //Checks whether a given username 'admin' has a given user role
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT user_id FROM sec.sec_users_n_roles WHERE ((user_id = " +
                    Global.getUserID(username) + ") AND (role_id = " + Global.myNwMainFrm.cmmnCode.getRoleID(rolename) +
                    "))";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool hasPlcyEvrHdThsMdl(int inp_plcy_id, int inp_mdl_id)
        {
            //Checks whether a given policy has a given module
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT policy_id FROM sec.sec_audit_trail_tbls_to_enbl WHERE ((policy_id = " +
                    inp_plcy_id + ") AND (module_id = " + inp_mdl_id + "))";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Int64 getUserID(string username)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select user_id from sec.sec_users where lower(user_name) = '" +
              username.Replace("'", "''").ToLower() + "'";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }
        public static Int64 getUserIDFrmTrnsCode(string usrTrnsCode)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select user_id from sec.sec_users where lower(COALESCE(code_for_trns_nums,'')) = '" +
              usrTrnsCode.Replace("'", "''").ToLower() + "' and COALESCE(code_for_trns_nums,'') != ''";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return Int64.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }
        public static string getUserPswd(string username)
        {
            //Example username 'admin'
            DataSet dtSt = new DataSet();
            string sqlStr = "select usr_password from sec.sec_users where lower(user_name) = '" +
              username.Replace("'", "''").ToLower() + "'";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static bool isUserAccntLckd(string username)
        {
            //Checks Whether a user's account is locked
            /*
             * Criteria for checking whether a user's account is locked
             * 1. Check if failed  attempts is greater than or equal to the set number
             */
            string sqlStr = "SELECT failed_login_atmpts >= " + Global.myNwMainFrm.cmmnCode.get_CurPlcy_Mx_Fld_lgns() +
            "  FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                if (bool.Parse(dtSt.Tables[0].Rows[0][0].ToString()) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool isAccntSuspended(string username)
        {
            //Checks Whether a user's account is suspended
            string sqlStr = "SELECT is_suspended FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                if (bool.Parse(dtSt.Tables[0].Rows[0][0].ToString()) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool isPswdTmp(string username)
        {
            //Checks Whether a user's password is temporary
            string sqlStr = "SELECT is_pswd_temp FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                if (bool.Parse(dtSt.Tables[0].Rows[0][0].ToString()) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static bool isPswdExpired(string username)
        {
            //Checks whether a user's password has expired
            /*For a password to expire
             * 1. the difference between the last pasword change time and now must be greater than
             *				the set number of expiry days
             * 2. 
             */
            string sqlStr = "SELECT age(now(), to_timestamp(last_pswd_chng_time, 'YYYY-MM-DD HH24:MI:SS')) " +
              ">= interval '" + Global.myNwMainFrm.cmmnCode.get_CurPlcy_Pwd_Exp_Days() + " days'" +
              " FROM sec.sec_users WHERE lower(user_name) = '" + username.Replace("'", "''").ToLower() + "'";
            DataSet dtSt = new DataSet();
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                if (bool.Parse(dtSt.Tables[0].Rows[0][0].ToString()) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #endregion

        #region "CUSTOM FUNCTIONS..."
        public static void refreshRqrdVrbls()
        {
            Global.myNwMainFrm.cmmnCode.DefaultPrvldgs = Global.dfltPrvldgs;
            //Global.myNwMainFrm.cmmnCode.Login_number = Global.mySecurity.login_number;
            Global.myNwMainFrm.cmmnCode.ModuleAdtTbl = Global.mySecurity.full_audit_trail_tbl_name;
            Global.myNwMainFrm.cmmnCode.ModuleDesc = Global.mySecurity.mdl_description;
            Global.myNwMainFrm.cmmnCode.ModuleName = Global.mySecurity.name;
            //Global.myNwMainFrm.cmmnCode.pgSqlConn = Global.mySecurity.Host.globalSQLConn;
            //Global.myNwMainFrm.cmmnCode.Role_Set_IDs = Global.mySecurity.role_set_id;
            //Global.myNwMainFrm.cmmnCode.Org_id = Global.mySecurity.org_id;
            Global.myNwMainFrm.cmmnCode.SampleRole = "System Administrator";
            //Global.myNwMainFrm.cmmnCode.User_id = Global.mySecurity.user_id;
            Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
            Global.mySecurity.user_id = Global.myNwMainFrm.usr_id;
            Global.mySecurity.login_number = Global.myNwMainFrm.lgn_num;
            Global.mySecurity.role_set_id = Global.myNwMainFrm.role_st_id;
            Global.mySecurity.org_id = Global.myNwMainFrm.Og_id;

        }

        public static void createSampleExtraInfos()
        {
            for (int i = 0; i < Global.subGrpsWithExtrInfo.Length; i++)
            {
                long tblID = Global.myNwMainFrm.cmmnCode.getMdlGrpTblID(Global.subGrpsWithExtrInfo[i],
                 Global.myNwMainFrm.cmmnCode.getModuleID(Global.mdlsWithExtrInfo[i]));
                int lovID = Global.myNwMainFrm.cmmnCode.getLovID("Extra Information Labels");
                for (int j = 0; j < Global.dfltExtraInfoNames.Length; j++)
                {
                    if (j % 2 == 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (i == int.Parse(Global.dfltExtraInfoNames[j - 1]))
                        {
                            int pssblValID = Global.myNwMainFrm.cmmnCode.getPssblValID(Global.dfltExtraInfoNames[j], lovID);
                            if (Global.doesTableHvThsExtrInfoLbl(tblID, pssblValID) == false)
                            {
                                Global.createAllwdExtraInfos(tblID, pssblValID, true);
                            }
                        }
                    }
                }
            }
        }

        public static void selectPerson(ref TextBox mytxt, ref TextBox myIDtxt, string srchWrd)
        {
            personDiag nwDiag = new personDiag();
            nwDiag.Location = new System.Drawing.Point(
              mytxt.Parent.Location.X + mytxt.Location.X + (int)(mytxt.Width),
              mytxt.Parent.Location.Y + mytxt.Location.Y + 22);
            long test = -1;
            if (long.TryParse(myIDtxt.Text, out test) == false)
            {
                nwDiag.selectPsn_ID = (-1);
            }
            else
            {
                nwDiag.selectPsn_ID = test;
            }
            nwDiag.srchWrd = srchWrd;
            DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == System.Windows.Forms.DialogResult.OK)
            {
                myIDtxt.Text = String.Format("{0}", nwDiag.selectPsn_ID);
                mytxt.Text = Global.getPrsnName(nwDiag.selectPsn_ID);
            }
        }

        public static string generatePswd()
        {
            string nwPswd = "";
            char[] alphLtrs = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
            'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            int[] dgts ={ 0, 1, 2, 3, 4, 5, 6, 7, 7, 8, 9, 10,
            11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            Random my_rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                if (i < 2)
                {
                    nwPswd = nwPswd + alphLtrs[my_rand.Next(0, 26)].ToString();
                }
                else if (i < 6)
                {
                    nwPswd = nwPswd + alphLtrs[my_rand.Next(0, 26)].ToString().ToLower();
                }
                else
                {
                    nwPswd = nwPswd + dgts[my_rand.Next(0, 26)].ToString();
                }
            }
            return nwPswd;
        }

        public static int getActnPrcssID(string rnngprcsID)
        {
            string strSql = @"SELECT process_id FROM accb.accb_running_prcses WHERE which_process_is_rnng = " + rnngprcsID + "";

            //Global.myNwMainFrm.cmmnCode.showMsg(strSql, 0);
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            if (dtst.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
            }
            return -1;
        }

        public static void createActnPrcss(int prcsID, string process_type)
        {
            string dtestr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string strSql = @"INSERT INTO accb.accb_running_prcses(
            which_process_is_rnng, last_active_time, process_type)
    VALUES (" + prcsID + ", '" + dtestr + "','" + process_type.Replace("'", "''") + "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(strSql);
        }

        public static void updtActnPrcss(int prcsID, string process_type)
        {
            Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
            Global.myNwMainFrm.cmmnCode.ignorAdtTrail = true;
            string dtestr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string strSql = @"UPDATE accb.accb_running_prcses SET
            last_active_time='" + dtestr + "', process_type='" + process_type.Replace("'", "''") + "' " +
                  "WHERE which_process_is_rnng = " + prcsID + " ";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(strSql);
            Global.myNwMainFrm.cmmnCode.ignorAdtTrail = false;
        }


        public static void updtOrgAccntCurrID(int orgID, int crncyID)
        {
            Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
            Global.myNwMainFrm.cmmnCode.ignorAdtTrail = true;
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string updtSQL = "UPDATE accb.accb_chart_of_accnts SET crncy_id = " + crncyID +
                              ", last_update_by = " + Global.mySecurity.user_id + ", " +
                              "last_update_date = '" + dateStr + "' " +
              "WHERE (org_id = " + orgID + " and crncy_id<=0)";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);
            updtSQL = @"UPDATE accb.accb_trnsctn_details SET dbt_or_crdt='C' WHERE dbt_or_crdt='U' and dbt_amount=0 and crdt_amount !=0;
UPDATE accb.accb_trnsctn_details SET dbt_or_crdt='D' WHERE dbt_or_crdt='U' and dbt_amount!=0 and crdt_amount =0;";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);
            updtSQL = @"UPDATE accb.accb_trnsctn_details SET entered_amnt=dbt_amount, accnt_crncy_amnt=dbt_amount WHERE dbt_amount!=0 and crdt_amount =0 and entered_amnt=0 and accnt_crncy_amnt=0;
UPDATE accb.accb_trnsctn_details SET entered_amnt=crdt_amount, accnt_crncy_amnt=crdt_amount WHERE dbt_amount=0 and crdt_amount!=0 and entered_amnt=0 and accnt_crncy_amnt=0";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);
            updtSQL = @"UPDATE accb.accb_trnsctn_details SET entered_amt_crncy_id=func_cur_id WHERE entered_amt_crncy_id=-1;
UPDATE accb.accb_trnsctn_details SET accnt_crncy_id=func_cur_id WHERE accnt_crncy_id=-1";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);
            updtSQL = @"UPDATE prs.prsn_names_nos SET org_id=" + orgID + " WHERE org_id=-1";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);
            Global.myNwMainFrm.cmmnCode.ignorAdtTrail = false;

        }

        public static void createDfltAcnts(int orgid)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string insSQL = "INSERT INTO scm.scm_dflt_accnts(" +
                  "itm_inv_asst_acnt_id, cost_of_goods_acnt_id, expense_acnt_id, " +
                  "prchs_rtrns_acnt_id, rvnu_acnt_id, sales_rtrns_acnt_id, sales_cash_acnt_id, " +
                  "sales_check_acnt_id, sales_rcvbl_acnt_id, rcpt_cash_acnt_id, " +
                  "rcpt_lblty_acnt_id, rho_name, org_id, created_by, creation_date, " +
                  "last_update_by, last_update_date, inv_adjstmnts_lblty_acnt_id) " +
                  "VALUES (-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,'Default Accounts', " +
                  orgid + ", " + Global.mySecurity.user_id + ", '" + dateStr +
                  "', " + Global.mySecurity.user_id + ", '" + dateStr +
                  "',-1)";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(insSQL);
        }

        public static void updtOrgInvoiceCurrID(int orgID, int crncyID, long pymtMthdID)
        {
            Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string updtSQL = "UPDATE scm.scm_sales_invc_hdr SET invc_curr_id = " + crncyID +
                              ", last_update_by = " + Global.mySecurity.user_id + ", " +
                              "last_update_date = '" + dateStr + "' " +
              "WHERE (org_id = " + orgID + " and invc_curr_id<=0)";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);
            updtSQL = "UPDATE scm.scm_sales_invc_hdr SET pymny_method_id = " + pymtMthdID +
                              ", last_update_by = " + Global.mySecurity.user_id + ", " +
                              "last_update_date = '" + dateStr + "' " +
              "WHERE (org_id = " + orgID + " and pymny_method_id<=0)";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);

        }

        public static void updateOrgnlSellingPrice()
        {
            Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string updtSQL = "UPDATE inv.inv_itm_list SET " +
                  "orgnl_selling_price = selling_price  WHERE (orgnl_selling_price = 0 and selling_price IS NOT NULL)";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);
        }

        public static void updateUOMPrices()
        {
            Global.myNwMainFrm.cmmnCode.Extra_Adt_Trl_Info = "";
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string updtSQL = @"UPDATE inv.itm_uoms SET 
            selling_price = scm.get_item_unit_sllng_price(item_id, 1)*cnvsn_factor, 
            price_less_tax=scm.get_item_unit_price_ls_tx(item_id, 1)*cnvsn_factor
      WHERE (selling_price = 0 and price_less_tax =0)";
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(updtSQL);
        }

        public static void createSysLovs()
        {
            for (int i = 0; i < Global.sysLovs.Length; i++)
            {
                System.Windows.Forms.Application.DoEvents();
                int lovID = Global.getLovID(sysLovs[i]);
                if (lovID <= 0)
                {
                    if (sysLovsDynQrys[i] == "")
                    {
                        Global.createLovNm(sysLovs[i],
                         sysLovsDesc[i], false, "", "SYS", true);
                    }
                    else
                    {
                        Global.createLovNm(sysLovs[i],
                   sysLovsDesc[i], true, sysLovsDynQrys[i], "SYS", true);
                    }
                }
                else
                {
                    if (sysLovsDynQrys[i] != "")
                    {
                        Global.updateLovNm(lovID, true, sysLovsDynQrys[i], "SYS", true);
                    }
                }
            }
        }

        public static void updateLovNm(int lovID, bool isDyn
          , string sqlQry, string dfndBy, bool isEnbld)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE gst.gen_stp_lov_names SET " +
                  "is_list_dynamic='" + Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(isDyn) + "', " +
                  "sqlquery_if_dyn='" + sqlQry.Replace("'", "''") +
          "', defined_by='" + dfndBy.Replace("'", "''") +
              "', last_update_by=" + Global.mySecurity.user_id + ", " +
                  "last_update_date='" + dateStr +
                  "', is_enabled='" + Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(isEnbld) + "' WHERE value_list_id = " + lovID;
            Global.myNwMainFrm.cmmnCode.updateDataNoParams(sqlStr);
        }

        public static void createSysLovsPssblVals()
        {
            string allwd = Global.get_all_OrgIDs();
            for (int i = 0; i < Global.pssblVals.Length; i += 3)
            {
                System.Windows.Forms.Application.DoEvents();
                int pssblValID = Global.getPssblValID(Global.pssblVals[i + 1],
                  Global.getLovID(Global.sysLovs[int.Parse(Global.pssblVals[i])]), Global.pssblVals[i + 2]);
                //, Global.pssblVals[i + 2]
                if (pssblValID <= 0)
                {
                    Global.createPssblValsForLov(Global.getLovID(Global.sysLovs[int.Parse(Global.pssblVals[i])]),
                      Global.pssblVals[i + 1], Global.pssblVals[i + 2], true, allwd);
                }
                else
                {
                    Global.updatePssblValsForLov(pssblValID, Global.pssblVals[i + 1], Global.pssblVals[i + 2]);
                }
            }
        }
        public static void createLovNm(string lovNm, string lovDesc, bool isDyn
          , string sqlQry, string dfndBy, bool isEnbld)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "INSERT INTO gst.gen_stp_lov_names(" +
                  "value_list_name, value_list_desc, is_list_dynamic, " +
                  "sqlquery_if_dyn, defined_by, created_by, creation_date, last_update_by, " +
                  "last_update_date, is_enabled) " +
              "VALUES ('" + lovNm.Replace("'", "''") + "', '" + lovDesc.Replace("'", "''") +
          "', '" + Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(isDyn) + "', '" + sqlQry.Replace("'", "''") + "', '" + dfndBy.Replace("'", "''") +
              "', " + Global.mySecurity.user_id + ", '" + dateStr + "', " + Global.mySecurity.user_id +
              ", '" + dateStr + "', '" + Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(isEnbld) + "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void createPssblValsForLov(int lovID, string pssblVal,
          string pssblValDesc, bool isEnbld, string allwd)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "INSERT INTO gst.gen_stp_lov_values(" +
                  "value_list_id, pssbl_value, pssbl_value_desc, " +
                              "created_by, creation_date, last_update_by, last_update_date, is_enabled, allowed_org_ids) " +
              "VALUES (" + lovID + ", '" + pssblVal.Replace("'", "''") + "', '" + pssblValDesc.Replace("'", "''") +
              "', " + Global.mySecurity.user_id + ", '" + dateStr + "', " + Global.mySecurity.user_id +
              ", '" + dateStr + "', '" +
              Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(isEnbld) +
              "', '" + allwd.Replace("'", "''") + "')";
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }
        public static void updatePssblValsForLov(int pssblValID, string pssblVal,
      string pssblValDesc)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE gst.gen_stp_lov_values SET " +
                  "pssbl_value='" + pssblVal.Replace("'", "''") +
                  "', pssbl_value_desc='" + pssblValDesc.Replace("'", "''") +
              "', last_update_by=" + Global.mySecurity.user_id +
              ", last_update_date='" + dateStr + "' WHERE pssbl_value_id = " + pssblValID;
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }

        public static void enblPssblValForLov(int pssblValID, bool isEnbld)
        {
            string dateStr = Global.myNwMainFrm.cmmnCode.getDB_Date_time();
            string sqlStr = "UPDATE gst.gen_stp_lov_values SET " +
                  "is_enabled='" + Global.myNwMainFrm.cmmnCode.cnvrtBoolToBitStr(isEnbld) +
              "', last_update_by=" + Global.mySecurity.user_id +
              ", last_update_date='" + dateStr + "' WHERE pssbl_value_id = " + pssblValID;
            Global.myNwMainFrm.cmmnCode.insertDataNoParams(sqlStr);
        }
        #endregion

        #region "VERIFICATION STATEMENTS..."
        public static int getLovID(string lovName)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT value_list_id from gst.gen_stp_lov_names where (value_list_name = '" +
              lovName.Replace("'", "''") + "')";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static string getLovName(int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT value_list_name from gst.gen_stp_lov_names where (value_list_id = " +
              lovID + ")";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static int getPssblValID(string pssblVal, int lovID, string pssblValDesc)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
              "where ((pssbl_value = '" +
              pssblVal.Replace("'", "''") + "') AND (pssbl_value_desc = '" +
              pssblValDesc.Replace("'", "''") + "') AND (value_list_id = " + lovID + "))";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static int getPssblValID(string pssblVal, int lovID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value_id from gst.gen_stp_lov_values " +
              "where ((pssbl_value = '" +
              pssblVal.Replace("'", "''") + "') AND (value_list_id = " + lovID + "))";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return int.Parse(dtSt.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return -1;
            }
        }

        public static string getPssblValNm(int pssblVlID)
        {
            DataSet dtSt = new DataSet();
            string sqlStr = "SELECT pssbl_value from gst.gen_stp_lov_values " +
              "where ((pssbl_value_id = " + pssblVlID + "))";
            dtSt = Global.myNwMainFrm.cmmnCode.selectDataNoParams(sqlStr);
            if (dtSt.Tables[0].Rows.Count > 0)
            {
                return dtSt.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string get_all_OrgIDs()
        {
            string strSql = "";
            strSql = "SELECT distinct org_id FROM org.org_details";
            DataSet dtst = Global.myNwMainFrm.cmmnCode.selectDataNoParams(strSql);
            string allwd = ",";
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                allwd += dtst.Tables[0].Rows[i][0].ToString() + ",";
            }
            return allwd;
        }

        #endregion
    }
}
