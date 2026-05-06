using System;
using System.Collections.Generic;
using System.Linq;

/* 引用元：
 https://atcoder.jp/contests/abc454/tasks/abc454_c
 */

namespace C_StrawMillionaire {
    internal class Program {
        static void Main(string[] args) {

            // 入力受付
            var inputs = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int friendCount = inputs[1];

            // アイテム情報を登録する
            var itemManager = new ItemManager();
            for (int i = 0; i < friendCount; i++) {
                var itemInfo = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                itemManager.AddExchangeableItem(itemInfo[0], itemInfo[1]);
            }

            // 出力処理
            Console.WriteLine(itemManager.CountGettableItems());
        }
    }

    /// <summary>
    /// アイテム
    /// </summary>
    internal class Item {

        /// <summary>
        /// 交換可能なアイテムのIDの集合
        /// </summary>
        private readonly HashSet<int> _exchangeableItemIds = new HashSet<int>();

        /// <summary>
        /// 交換可能なアイテムのIDの集合
        /// </summary>
        public IReadOnlyCollection<int> ExchangeableItemIds => _exchangeableItemIds;

        /// <summary>
        /// 交換可能なアイテムを追加する
        /// </summary>
        public void AddExchangeableItem(int itemId) => _exchangeableItemIds.Add(itemId);
    }

    /// <summary>
    /// アイテム管理者
    /// </summary>
    internal class ItemManager {

        /// <summary>
        /// 初めに与えられるアイテムのID
        /// </summary>
        private static readonly int FIRST_ITEM_ID = 1;

        /// <summary>
        /// アイテムIDからアイテムを取得するためのマップ
        /// </summary>
        private readonly Dictionary<int, Item> _idToItem = new Dictionary<int, Item>();

        /// <summary>
        /// IDからアイテムを取得する。存在しない場合は新規作成して返す
        /// </summary>
        private Item GetItemById(int itemId) {
            if (!_idToItem.TryGetValue(itemId, out var item)) {
                item = new Item();
                _idToItem[itemId] = item;
            }
            return item;
        }

        /// <summary>
        /// アイテムに対して交換可能なアイテムを追加する
        /// </summary>
        public void AddExchangeableItem(int itemId, int exchangeableItemId) {
            Item item = this.GetItemById(itemId);
            item.AddExchangeableItem(exchangeableItemId);
        }

        /// <summary>
        /// 初めに与えられるアイテムから交換可能なアイテムを幅優先探索ですべて数える
        /// </summary>
        public int CountGettableItems() {
            var visited = new HashSet<int>();
            var queue = new Queue<int>();
            queue.Enqueue(FIRST_ITEM_ID);
            visited.Add(FIRST_ITEM_ID);
            while (queue.Count > 0) {
                var currentId = queue.Dequeue();
                if (!_idToItem.TryGetValue(currentId, out var item)) continue;
                foreach (var nextId in item.ExchangeableItemIds) {
                    if (!visited.Add(nextId)) continue;
                    queue.Enqueue(nextId);
                }
            }
            return visited.Count;
        }
    }
}
