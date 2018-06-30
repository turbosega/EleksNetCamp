using System;
using WebApi.Extensions;

namespace WebApi.Models.Entities
{
    public class Score
    {
        public int Id     { get; set; }
        public int Result { get; set; }

        public int UserId { get; set; }
        public int GameId { get; set; }

        private User _user;
        private Game _game;

        public User User
        {
            get => LazyLoader.Load(this, ref _user);
            set => _user = value;
        }

        public Game Game
        {
            get => LazyLoader.Load(this, ref _game);
            set => _game = value;
        }

        public Score()
        {
        }

        private Action<object, string> LazyLoader { get; set; }

        private Score(Action<object, string> lazyLoader) => LazyLoader = lazyLoader;
    }
}