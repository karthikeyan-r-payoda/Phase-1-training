
CREATE TABLE LMS_MEMBERS (
    member_id VARCHAR(10) PRIMARY KEY,
    member_name VARCHAR(100),
    city VARCHAR(20),
    membership_status VARCHAR(15),
    date_registration DATE,
    date_expiry DATE
);

CREATE TABLE LMS_SUPPLIERS_DETAILS (
    supplier_id VARCHAR(10) PRIMARY KEY,
    supplier_name VARCHAR(50),
    contact VARCHAR(15),
    email VARCHAR(100),
    address VARCHAR(100)
);

CREATE TABLE LMS_FINE_DETAILS (
    fine_range VARCHAR(50)PRIMARY KEY,
    fine_amount INT
);


CREATE TABLE LMS_BOOK_DETAILS (
    book_code VARCHAR(10) PRIMARY KEY,
    book_title VARCHAR(150),
    author VARCHAR(30),
    category VARCHAR(50),
    book_edition INT,
    price INT,
    publish_date DATE,
    date_arrival DATE,
    publication varchar(20),
    rack_number VARCHAR(10),
    supplier_id VARCHAR(10),
    FOREIGN KEY (supplier_id) REFERENCES LMS_SUPPLIERS_DETAILS(supplier_id)
);

CREATE TABLE LMS_BOOK_ISSUE (
    book_issue_no INT AUTO_INCREMENT PRIMARY KEY,
    member_id VARCHAR(10),
    book_code VARCHAR(10),
    date_issue DATE,
    due_return DATE,
    date_returned DATE,
    book_issue_status VARCHAR(1),
    fine_range varchar(50),
    FOREIGN KEY (member_id) REFERENCES LMS_MEMBERS(member_id),
    FOREIGN KEY (book_code) REFERENCES LMS_BOOK_DETAILS(book_code),
    FOREIGN KEY (fine_range) REFERENCES LMS_FINE_DETAILS(fine_range)
);



-- Simple

select member_id,member_name,city,membership_status from lms_members
where membership_status='Permanent';


select distinct m.member_id,m.member_name from lms_members m
inner join lms_book_issue bi on bi.member_id=m.member_id
where book_issue_status='N';


select member_id,member_name from lms_members where member_id in
(select member_id from lms_book_issue where book_code='BL000002');

select book_code,book_title,author from lms_book_details where author like 'P%';

select count(category) NO_OF_BOOKS from lms_book_details where category='JAVA';




select category,count(category) NO_OF_BOOKS from lms_book_details group by category;




select count(publication) NO_OF_BOOKS from lms_book_details where publication='Prentice Hall';






select book_code,book_title from lms_book_details where book_code in
(select book_code from lms_book_issue where date_issue='2012-04-01');




select member_id,member_name,date_registration,date_expiry from lms_members
where date_expiry < '2013-04-01';

select member_id,member_name,date_registration,membership_status from lms_members
where date_registration < '2012-03-01' and membership_status='Temporary';

select member_id,upper(member_name) Name from lms_members 
where city='Chennai' or city='Delhi';

select distinct concat(book_title,' ','is written by',' ',author) BOOK_WRITTEN_BY 
from lms_book_details;




select avg(price) AVERAGEPRICE from lms_book_details where category='JAVA';

select supplier_id,supplier_name from lms_suppliers_details where email like '%@gmail.com';



select supplier_id,supplier_name,coalesce(email,contact,address) CONTACTDETAILS from lms_suppliers_details ;


select supplier_id,supplier_name,if(contact IS NULL,'No','Yes') PHONENUMAVAILABLE from lms_suppliers_details;


-- Average

select m.member_id,m.member_name,bi.book_code,bd.book_title from lms_members m
join lms_book_issue bi on bi.member_id=m.member_id
join  lms_book_details bd on bd.book_code =bi.book_code;




select count(*) NO_OF_BOOKS_AVAILABLE from lms_book_details where book_code not in
(select book_code from lms_book_issue);

select m.member_id,m.member_name,bi.fine_range,fd.fine_amount from lms_members m
join lms_book_issue bi on bi.member_id=m.member_id
join lms_fine_details fd on fd.fine_range=bi.fine_range
where fd.fine_amount<100;

select bd.book_code,bd.book_title,bi.book_issue_status as AVAILABILITYSTATUS from lms_book_details bd
join lms_book_issue bi on bi.book_code=bd.book_code
where category='JAVA' and book_edition=6;


select book_code,book_title,rack_number from lms_book_details 
where rack_number='A1' 
order by book_title;

select m.member_id,m.member_name,bi.due_return,bi.date_returned from lms_members m
join lms_book_issue bi on bi.member_id = m.member_id
where bi.date_returned >  bi.due_return ;

select m.member_id,m.member_name from lms_members m where member_id not in
( select member_id from lms_book_issue);


select m.member_id,m.member_name from lms_members m join lms_book_issue i on
i.member_id=m.member_id
where i.date_returned < i.date_returned and year(i.date_issue)=2012;

select date_issue,count(*) NOOFBOOKS from lms_book_issue 
group by date_issue 
order by NOOFBOOKS desc 
limit 1;

select b.book_title,b.supplier_id from lms_book_details b
where b.author='Herbert Schildt' and b.supplier_id='S01' and b.book_edition=5;


select b.rack_number,count(*) NOOFBOOKS from lms_book_details b
group by b.rack_number 
order by b.rack_number;

select I.book_issue_no, M.member_name, M.date_registration, M.date_expiry, D.book_title, D.category, D.author,
 D.price, I.date_issue as 'date of issue', I.due_return 'date of return', I.date_returned 'Actual Returned date',
 I.book_issue_status 'Issue Satus', F.fine_amount FROM LMS_BOOK_ISSUE I 
JOIN LMS_MEMBERS M ON I.member_id = M.member_id 
JOIN LMS_BOOK_DETAILS D ON I.book_code = D.book_code 
LEFT JOIN LMS_FINE_DETAILS F ON I.fine_range = F.fine_range;

select book_code,book_title,publish_date from lms_book_details 
where Month(publish_date) = 12;

select b.book_code,book_title,i.book_issue_status as AVAILABILITYSTATUS from lms_book_details b
join lms_book_issue i on i.book_code=b.book_code
where b.category='JAVA' AND b.book_edition=5;


-- Complex

SELECT D.book_code, D.book_title, S.supplier_name FROM LMS_BOOK_DETAILS D JOIN LMS_SUPPLIERS_DETAILS S ON
 D.supplier_id = S.supplier_id WHERE D.supplier_id = (SELECT supplier_id FROM LMS_BOOK_DETAILS 
 GROUP BY supplier_id ORDER BY COUNT(*) DESC LIMIT 1);

SELECT M.member_id, M.member_name, 3 - COUNT(B.book_code) AS REMAININGBOOKS FROM LMS_MEMBERS M 
LEFT JOIN LMS_BOOK_ISSUE B ON M.member_id = B.member_id GROUP BY M.member_id, M.member_name;


SELECT supplier_id, supplier_name FROM LMS_SUPPLIERS_DETAILS WHERE 
supplier_id = (SELECT supplier_id FROM LMS_BOOK_DETAILS 
GROUP BY supplier_id ORDER BY COUNT(*) ASC LIMIT 1);
