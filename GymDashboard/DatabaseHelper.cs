using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace GymDashboard
{
    public static class DatabaseHelper
    {
        private static string dbPath = "gym.db";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                using (var conn = GetConnection())
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"
                        CREATE TABLE GymEquipment (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL
                        );

                        CREATE TABLE UsageLog (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            EquipmentId INTEGER,
                            DateUsed TEXT,
                            DurationMinutes INTEGER,
                            FOREIGN KEY (EquipmentId) REFERENCES GymEquipment(Id)
                        );

                        INSERT INTO GymEquipment (Name) VALUES 
                            ('Беговая дорожка'), ('Эллипсоид'), ('Лестница'),
                            ('Велотренажер'), ('Бабочка'), ('Тренажер Смита'),
                            ('Гребной тренажер'), ('Жим ногами (платформа)');

                        INSERT INTO UsageLog (EquipmentId, DateUsed, DurationMinutes)
                        VALUES 
                            (1, '2025-05-01', 30),
                            (2, '2025-05-01', 20),
                            (1, '2025-05-02', 25),
                            (3, '2025-05-02', 15),
                            (5, '2025-05-03', 40);
                    ";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection($"Data Source={dbPath};Version=3;");
        }

        public static List<UsageEntry> GetUsageData(string date = null)
        {
            var list = new List<UsageEntry>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    SELECT g.Name, u.DateUsed, SUM(u.DurationMinutes) AS Duration
                    FROM UsageLog u
                    JOIN GymEquipment g ON g.Id = u.EquipmentId
                    WHERE (@date IS NULL OR u.DateUsed = @date)
                    GROUP BY g.Name, u.DateUsed
                    ORDER BY u.DateUsed";

                cmd.Parameters.AddWithValue("@date", string.IsNullOrEmpty(date) ? DBNull.Value : (object)date);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UsageEntry
                        {
                            EquipmentName = reader.GetString(0),
                            DateUsed = reader.GetString(1),
                            DurationMinutes = reader.GetInt32(2)
                        });
                    }
                }
            }
            return list;
        }
    }
}
