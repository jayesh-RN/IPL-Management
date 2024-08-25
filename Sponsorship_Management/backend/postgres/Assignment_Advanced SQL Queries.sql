-- Retrieve the total contract value for each sponsor, but only for sponsors who have at least one completed payment. Display the Sponsorname and the TotalContractValue

select * from spon_manag.Sponsors;
select * from spon_manag.Contracts;
select * from spon_manag.Payments;


SELECT s.SponsorName, SUM(c.ContractValue) AS TotalContractValue
FROM spon_manag.Sponsors s
JOIN spon_manag.Contracts c ON s.SponsorID = c.SponsorID
JOIN spon_manag.Payments p ON c.ContractID = p.ContractID
WHERE p.PaymentStatus = 'Completed'
GROUP BY s.SponsorName


-- Retrieve sponsors who have sponsored more than one match, along with the total number of matches they have sponsored. Display the Sponsor name and Number of Matches.
select * from spon_manag.Sponsors;
select * from spon_manag.Contracts;

SELECT s.SponsorName, COUNT(c.matchid) AS MatchesSponsors
FROM spon_manag.Sponsors s
JOIN spon_manag.Contracts c ON s.SponsorID = c.SponsorID
GROUP BY s.SponsorName
HAving COUNT(c.MatchID) > 1;





--   Write an SQL query that retrieves a list of all sponsors along with their total contract value. Additionally, categorize each sponsor based on the total value of their contracts using the following criteria:

-- If the total contract value is greater than $500,000, label the sponsor as 'Platinum'.

-- If the total contract value is between $200,000 and $500,000, label the sponsor as 'Gold'.

-- If the total contract value is between $100,000 and $200,000, label the sponsor as 'Silver'.

-- If the total contract value is less than $100,000, label the sponsor as 'Bronze'.



SELECT s.SponsorName , SUM(c.ContractValue) as Contract_Value ,
	(case 
		when (SUM(c.ContractValue) > 500000) then 'Platinum' 
		when (SUM(c.ContractValue) BETWEEN 200000 AND 500000) THEN 'Gold' 
		when (SUM(c.ContractValue) BETWEEN 100000 AND 200000) THEN 'Silver' 
		else 'Bronze'
	end
		) AS sponsor_category 
FROM spon_manag.Sponsors s
JOIN spon_manag.Contracts c ON s.SponsorID = c.SponsorID
GROUP BY s.SponsorName
HAVING SUM(c.ContractValue) > (SELECT AVG(c.ContractValue) AS AvgValue FROM spon_manag.Contracts c);

-- Retrieve Matches Where the Average Contract Value is Greater Than the Average Contract Value of All Matches. Display the match name and average contract value.
select * from spon_manag.Matches;
select * from spon_manag.Contracts;


SELECT m.matchName , AVG(c.ContractValue) as Average_Value FROM spon_manag.Matches m
JOIN spon_manag.Contracts c ON m.matchID = c.matchID
GROUP BY m.matchname
HAVING AVG(c.ContractValue) > (SELECT AVG(c.ContractValue) FROM spon_manag.Contracts c);


-- Find Sponsors Who Have the Highest Total Payments for a Single Match.Display the sponsor name, match name and total amount paid
select * from spon_manag.Matches;
select * from spon_manag.Contracts;
select * from spon_manag.Sponsors;
select * from spon_manag.Payments;

SELECT s.SponsorName, m.MatchName, SUM(p.AmountPaid) AS total_amount_paid
FROM spon_manag.Sponsors s
JOIN spon_manag.Contracts c ON s.SponsorID = c.SponsorID
JOIN spon_manag.Matches m ON c.MatchID = m.MatchID
JOIN spon_manag.Payments p ON c.ContractID = p.ContractID
GROUP BY (s.SponsorName, m.matchID)
ORDER BY 
  SUM(p.AmountPaid) DESC
LIMIT 1;



