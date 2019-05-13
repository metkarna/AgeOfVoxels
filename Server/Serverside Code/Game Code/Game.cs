using System;
using System.Collections.Generic;
using PlayerIO.GameLibrary;

namespace MushroomsUnity3DExample {
    // Игрок, отнаследованный от базового
    public class Player : BasePlayer {
        public bool red = false;
        public bool blue = false;
	}

    // Тип комнаты
	[RoomType("UnityMushrooms")]
	public class GameCode : Game<Player> {
        private Player red = null;
        private Player blue = null; 

		// This method is called when an instance of your t9he game is created
		public override void GameStarted() {
			Console.WriteLine("Game is started: " + RoomId);
		}

        // Закрываем игру, когда ласт игрок выходит
		public override void GameClosed() {
			Console.WriteLine("RoomId: " + RoomId);
		}

		// Вызываем, когда игрок заходит в игру
		public override void UserJoined(Player player) {
            foreach (Player pl in Players)
            {
                if (pl.ConnectUserId != player.ConnectUserId)
                {
                    if (red == null) red = player;
                    else blue = player;
                }
            }
        }

		// Вызываем, когда игрок выходит из игры
		public override void UserLeft(Player player) {
			Broadcast("PlayerLeft", player.ConnectUserId);
		}

        // Вызываем, когда игрок отправляет сообщение на сервер (сюда)
		public override void GotMessage(Player player, Message message) {
			switch(message.Type) {
                // Методы, используемые игроком:
                case "tsUnitCreate":
                    Broadcast("fsUnitCreate", message.GetString(0), message.GetString(1));
                    break;
                case "tsCastleHP":
                    Broadcast("fsCastleHP");
                    break;
                case "TowerCreate":
                    break;
			}
		}
	}
}