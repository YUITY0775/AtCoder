using System;
using System.Collections.Generic;
using System.Text;

/*
 <問題>
 英小文字からなる N 個の文字列 S(1), S(2), …, S(N) が与えられます。ここで、文字列の長さはそれぞれ相異なります。
 これらの文字列を長さの昇順に並べ替え、この順に結合して得られる文字列を求めてください。

 <制約>
 ・2 ≤ N ≤ 50
 ・N は整数
 ・S(i) は長さ 1 以上 50 以下の英小文字からなる文字列
 ・i != j のとき S(i) と S(j) の長さは異なる

 <入力>
 N
 S(1)
 S(2)
 …
 S(N)

 <出力>
 答えを出力せよ。
 */

namespace B_cat {
    internal class Program {
        static void Main(string[] args) {
            int wSentencesCount = int.Parse(Console.ReadLine());
            var wSentences = new BinaryTree();
            for (int i = 0; i < wSentencesCount; i++) {
                wSentences.Insert(Console.ReadLine());
            }
            var wAnswerBuilder = new StringBuilder();
            foreach (TreeNode wNode in wSentences.InOrderTraversal()) {
                wAnswerBuilder.Append(wNode.Value);
            }
            Console.WriteLine(wAnswerBuilder);
        }
    }

    /// <summary>
    /// ノード
    /// </summary>
    internal class TreeNode {
        /// <summary>
        /// 値
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 左の子
        /// </summary>
        public TreeNode Left { get; set; }
        /// <summary>
        /// 右の子
        /// </summary>
        public TreeNode Right { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vValue">値</param>
        public TreeNode(string vValue) {
            this.Value = vValue;
            this.Left = null;
            this.Right = null;
        }
    }

    /// <summary>
    /// 二分探索木
    /// </summary>
    internal class BinaryTree {
        /// <summary>
        /// 根
        /// </summary>
        private TreeNode Root { get; set; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BinaryTree() {
            this.Root = null;
        }
        /// <summary>
        /// 挿入
        /// </summary>
        /// <param name="vValue">値</param>
        public void Insert(string vValue) {
            this.Root = InsertRecursive(this.Root, vValue);
        }
        /// <summary>
        /// 再帰的にノードを挿入するヘルパーメソッド
        /// </summary>
        private TreeNode InsertRecursive(TreeNode vNode, string vValue) {
            if (vNode == null) return new TreeNode(vValue);
            if (vValue.Length <= vNode.Value.Length) {
                vNode.Left = InsertRecursive(vNode.Left, vValue);
            } else {
                vNode.Right = InsertRecursive(vNode.Right, vValue);
            }
            return vNode;
        }
        /// <summary>
        /// データを昇順に列挙する
        /// </summary>
        public IEnumerable<TreeNode> InOrderTraversal() {
            if (this.Root == null) {
                throw new NullReferenceException(nameof(this.Root));
            }
            return EnumerateNodes(this.Root);
        }
        /// <summary>
        /// データを昇順に列挙するためのヘルパーメソッド
        /// </summary>
        private IEnumerable<TreeNode> EnumerateNodes(TreeNode vNode) {
            if (vNode == null) yield break;
            // 左の子 → 親 → 右の子 の順で列挙する
            foreach (TreeNode wLeftNode in EnumerateNodes(vNode.Left)) yield return wLeftNode;
            yield return vNode;
            foreach (TreeNode wRightNode in EnumerateNodes(vNode.Right)) yield return wRightNode;
        }
    }
}
