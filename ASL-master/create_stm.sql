select Sched_Date as Scheduled, customer.Address1, Name, Phone_Number, BillSubTotal as 'Bill Subtotal'
from service
join customer on customer.Id = service.CustId
where Sched_Date <= curdate() and comp_date ='0001-01-01';
 


