-- Create Teams table
CREATE TABLE ipl.Teams (
  team_id SERIAL PRIMARY KEY,
  team_name VARCHAR(50) UNIQUE NOT NULL,
  coach VARCHAR(50) NOT NULL,
  home_ground VARCHAR(100) NOT NULL,
  founded_year INTEGER NOT NULL,
  owner VARCHAR(50) NOT NULL
);

-- Create Players table
CREATE TABLE ipl.Players (
  player_id SERIAL PRIMARY KEY,
  player_name VARCHAR(50) NOT NULL,
  team_id INTEGER NOT NULL,
  role VARCHAR(30) NOT NULL,
  age INTEGER NOT NULL,
  matches_played INTEGER NOT NULL,
  FOREIGN KEY (team_id) REFERENCES ipl.Teams(team_id)
);

-- Create Matches table
CREATE TABLE ipl.Matches (
  match_id SERIAL PRIMARY KEY,
  match_date DATE NOT NULL,
  venue VARCHAR(100) NOT NULL,
  team1_id INTEGER NOT NULL,
  team2_id INTEGER NOT NULL,
  winner_team_id INTEGER,
  FOREIGN KEY (team1_id) REFERENCES ipl.Teams(team_id),
  FOREIGN KEY (team2_id) REFERENCES ipl.Teams(team_id),
  FOREIGN KEY (winner_team_id) REFERENCES ipl.Teams(team_id)
);

-- Create Fan_Engagement table
CREATE TABLE ipl.Fan_Engagement (
  engagement_id SERIAL PRIMARY KEY,
  match_id INTEGER NOT NULL,
  fan_id INTEGER NOT NULL,
  engagement_type VARCHAR(50) NOT NULL,
  engagement_time TIMESTAMP NOT NULL,
  FOREIGN KEY (match_id) REFERENCES ipl.Matches(match_id)
);








-- Insert into Teams
INSERT INTO ipl.Teams (team_name, coach, home_ground, founded_year, owner) 
VALUES 
('Mumbai Indians', 'Mahela Jayawardene', 'Wankhede Stadium', 2008, 'Reliance Industries'),
('Chennai Super Kings', 'Stephen Fleming', 'M. A. Chidambaram Stadium', 2008, 'India Cements'),
('Royal Challengers Bangalore', 'Sanjay Bangar', 'M. Chinnaswamy Stadium', 2008, 'United Spirits'),
('Kolkata Knight Riders', 'Brendon McCullum', 'Eden Gardens', 2008, 'Red Chillies Entertainment'),
('Delhi Capitals', 'Ricky Ponting', 'Arun Jaitley Stadium', 2008, 'GMR Group & JSW Group');

-- Insert into Players
INSERT INTO ipl.Players (player_name, team_id, role, age, matches_played) 
VALUES 
('Rohit Sharma', 1, 'Batsman', 36, 227),
('Jasprit Bumrah', 1, 'Bowler', 30, 120),
('MS Dhoni', 2, 'Wicketkeeper-Batsman', 42, 234),
('Ravindra Jadeja', 2, 'All-Rounder', 35, 210),
('Virat Kohli', 3, 'Batsman', 35, 237),
('AB de Villiers', 3, 'Batsman', 40, 184),
('Andre Russell', 4, 'All-Rounder', 36, 140),
('Sunil Narine', 4, 'Bowler', 35, 144),
('Rishabh Pant', 5, 'Wicketkeeper-Batsman', 26, 98),
('Shikhar Dhawan', 5, 'Batsman', 38, 206);

-- Insert into Matches
INSERT INTO ipl.Matches (match_date, venue, team1_id, team2_id, winner_team_id) 
VALUES 
('2024-04-01', 'Wankhede Stadium', 1, 2, 1),
('2024-04-05', 'M. A. Chidambaram Stadium', 2, 3, 3),
('2024-04-10', 'M. Chinnaswamy Stadium', 3, 4, 4),
('2024-04-15', 'Eden Gardens', 4, 5, 4),
('2024-04-20', 'Arun Jaitley Stadium', 5, 1, 1),
('2024-04-25', 'Wankhede Stadium', 1, 3, 3),
('2024-05-01', 'M. A. Chidambaram Stadium', 2, 5, 2),
('2024-05-05', 'M. Chinnaswamy Stadium', 3, 1, 1),
('2024-05-10', 'Eden Gardens', 4, 2, 2),
('2024-05-15', 'Arun Jaitley Stadium', 5, 4, 4);

-- Insert into Fan_Engagement
INSERT INTO ipl.Fan_Engagement (match_id, fan_id, engagement_type, engagement_time) 
VALUES 
(1, 101, 'Tweet', '2024-04-01 18:30:00'),
(1, 102, 'Like', '2024-04-01 18:35:00'),
(2, 103, 'Comment', '2024-04-05 20:00:00'),
(2, 104, 'Share', '2024-04-05 20:05:00'),
(3, 105, 'Tweet', '2024-04-10 16:00:00'),
(3, 106, 'Like', '2024-04-10 16:05:00'),
(4, 107, 'Comment', '2024-04-15 21:00:00'),
(4, 108, 'Share', '2024-04-15 21:10:00'),
(5, 109, 'Tweet', '2024-04-20 19:00:00'),
(5, 110, 'Like', '2024-04-20 19:05:00'),
(6, 111, 'Comment', '2024-04-25 20:00:00'),
(6, 112, 'Share', '2024-04-25 20:10:00'),
(7, 113, 'Tweet', '2024-05-01 18:00:00'),
(7, 114, 'Like', '2024-05-01 18:05:00'),
(8, 115, 'Comment', '2024-05-05 19:30:00'),
(8, 116, 'Share', '2024-05-05 19:35:00'),
(9, 117, 'Tweet', '2024-05-10 20:30:00'),
(9, 118, 'Like', '2024-05-10 20:35:00'),
(10, 119, 'Comment', '2024-05-15 21:45:00'),
(10, 120, 'Share', '2024-05-15 21:50:00');



select * from ipl.Teams;
select * from ipl.Players;
select * from ipl.Matches;
select * from ipl.Fan_Engagement;


INSERT INTO ipl.Players (player_name, team_id, role, age, matches_played)
SELECT 'New Player', 100, 'Batsman', 28, 0
FROM ipl.Teams
WHERE team_id = 100;



SELECT 
  M.match_id,
  T1.team_name AS team1_name,
  T2.team_name AS team2_name,
  M.venue,
  M.match_date,
  COUNT(FE.engagement_id) AS total_fan_engagements
FROM 
  ipl.Matches M
  JOIN ipl.Teams T1 ON M.team1_id = T1.team_id
  JOIN ipl.Teams T2 ON M.team2_id = T2.team_id
  LEFT JOIN ipl.Fan_Engagement FE ON M.match_id = FE.match_id
GROUP BY 
  M.match_id, T1.team_name, T2.team_name, M.venue, M.match_date
ORDER BY 
  M.match_id;



SELECT 
  P.player_name, 
  COUNT(M.match_id) AS matches_played, 
  SUM(FE.engagement_id) AS total_fan_engagements
FROM 
  ipl.Players P
  JOIN ipl.Matches M ON P.team_id = M.team1_id OR P.team_id = M.team2_id
  JOIN ipl.Fan_Engagement FE ON M.match_id = FE.match_id
GROUP BY 
  P.player_name
ORDER BY 
  matches_played DESC, 
  total_fan_engagements DESC
LIMIT 5;



SELECT 
  M.match_id, 
  M.match_date, 
  M.venue, 
  T1.team_name AS team1_name, 
  T2.team_name AS team2_name
FROM 
  ipl.Matches M
  JOIN ipl.Teams T1 ON M.team1_id = T1.team_id
  JOIN ipl.Teams T2 ON M.team2_id = T2.team_id
WHERE 
  M.match_date BETWEEN '2024-04-01' AND '2024-04-20'
ORDER BY 
  M.match_date;