using System;
using System.Collections.Generic;

/* 引用元：
 https://atcoder.jp/contests/abc442/tasks/abc442_b
 */

namespace B_MusicPlayer {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            int queriesCount = int.Parse(Console.ReadLine());

            // クエリ処理
            var player = new MusicPlayer();
            var answers = new List<string>();
            for (int i = 0; i < queriesCount; i++) {
                int commandType = int.Parse(Console.ReadLine());
                ICommand command = CommandFactory.Create(commandType);
                command.Execute(player);

                answers.Add(player.IsPlaying && player.Volume >= 3 ? "Yes" : "No");
            }

            // 出力処理
            Console.WriteLine(string.Join(Environment.NewLine, answers));
        }
    }

    #region ミュージックプレイヤー

    /// <summary>
    /// ミュージックプレイヤー
    /// </summary>
    internal class MusicPlayer {

        /// <summary>
        /// 曲が再生中か否か
        /// </summary>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// 音量
        /// </summary>
        public int Volume { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MusicPlayer() {
            this.IsPlaying = false;
            this.Volume = 0;
        }

        /// <summary>
        /// 音量を 1 増やす
        /// </summary>
        public void IncreaseVolume() => ++this.Volume;

        /// <summary>
        /// 音量を 1 減らす
        /// </summary>
        public void DecreaseVolume() => this.Volume = Math.Max(0, --this.Volume);

        /// <summary>
        /// 再生・一時停止を切り替える
        /// </summary>
        public void TogglePlayPause() => this.IsPlaying = !this.IsPlaying;
    }

    #endregion

    #region コマンド関連

    /// <summary>
    /// コマンド
    /// </summary>
    interface ICommand {

        /// <summary>
        /// コマンド実行
        /// </summary>
        void Execute(MusicPlayer player);
    }

    /// <summary>
    /// 音量UPコマンド
    /// </summary>
    internal class VolumeUpCommand : ICommand {

        /// <summary>
        /// コマンド実行
        /// </summary>
        public void Execute(MusicPlayer player) => player.IncreaseVolume();
    }

    /// <summary>
    /// 音量DOWNコマンド
    /// </summary>
    internal class VolumeDownCommand : ICommand {

        /// <summary>
        /// コマンド実行
        /// </summary>
        public void Execute(MusicPlayer player) => player.DecreaseVolume();
    }

    /// <summary>
    /// 再生・一時停止切替コマンド
    /// </summary>
    internal class TogglePlayPauseCommand : ICommand {

        /// <summary>
        /// コマンド実行
        /// </summary>
        public void Execute(MusicPlayer player) => player.TogglePlayPause();
    }

    /// <summary>
    /// コマンド作成役
    /// </summary>
    internal static class CommandFactory {

        /// <summary>
        /// 入力に応じたコマンドを作成する
        /// </summary>
        public static ICommand Create(int commandType) {
            switch (commandType) {
                case 1:
                    return new VolumeUpCommand();
                case 2:
                    return new VolumeDownCommand();
                case 3:
                    return new TogglePlayPauseCommand();
                default:
                    throw new ArgumentException("不正なコマンドです。");
            }
        }
    }

    #endregion
}
