using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/* 
	最初の日付は 2010-05-14

 */


namespace PrimeNumberModTable
{
	class Program
	{
		static void Main(string[] args)
		{
			Int32 primNumber = 17;
			if ( args.Length > 0 ) {
				primNumber = Convert.ToInt32( args[ 0 ] );
			}
			ModTable program = new ModTable( primNumber );
			program.Exec();

		}
	}

	internal class ModTable
	{
		private Int32 mPrimeNumber{ get; set; }
		private Int32 mMaxKeta{ get; set; }
		private Int32 mMaxKeta_plus_1{ get; set; }

		public ModTable( Int32 primeNumber )
		{
			mPrimeNumber = primeNumber;
			//	桁数
			mMaxKeta = (int)( Math.Log( mPrimeNumber, 10 ) + 1 );
			mMaxKeta_plus_1 = mMaxKeta + 1;
		}
		
		public void Exec()
		{
			PrintTableMultiMod();
			Console.WriteLine( "" );
			PrintTableExpMod();
		}

		// 掛け算の mod
		private void PrintTableMultiMod()
		{
			// 先頭行
			Console.Write( new string( ' ', mMaxKeta_plus_1 ) + "|" );
			var format_str_space = String.Format( "{{0,{0} }} ", mMaxKeta_plus_1 );
			var format_str_bar   = String.Format( "{{0,{0}}}|", mMaxKeta_plus_1 );
			
			for ( int i = 0; i < mPrimeNumber; i++ ) {
				Console.Write( format_str_space, i );
			}
			Console.WriteLine( "" );
			Console.WriteLine( new string( '-', ( mMaxKeta_plus_1 + 1 ) + ( mMaxKeta_plus_1 + 1 ) * ( mPrimeNumber ) ) );

			// 数表
			for ( int i = 0; i < mPrimeNumber; i++ ) {
				Console.Write( format_str_bar, i );

				Int32 mod = 0;
				for ( int j = 0; j < mPrimeNumber; j++ ) {

					Console.Write( format_str_space, mod );

					mod += i;
					if ( mod >= mPrimeNumber ) {
						mod -= mPrimeNumber;
					}
				}
				Console.WriteLine( "" );
			}
		}

		// 指数の mod
		private void PrintTableExpMod()
		{
			// 先頭行
			Console.Write( new string( ' ', mMaxKeta_plus_1 ) + "|" );
			var format_str_space = String.Format( "{{0,{0} }} ", mMaxKeta_plus_1 );
			var format_str_bar   = String.Format( "{{0,{0}}}|", mMaxKeta_plus_1 );
			
			for ( int i = 1; i < mPrimeNumber; i++ ) {
				Console.Write( format_str_space, i );
			}
			Console.WriteLine( "" );
			Console.WriteLine( new string( '-', ( mMaxKeta_plus_1 + 1 ) + ( mMaxKeta_plus_1 + 1 ) * ( mPrimeNumber - 1 ) ) );

			// 数表
			var primitiveRoots = new List< int >();
			for ( int i = 1; i < mPrimeNumber; i++ ) {
				Console.Write( format_str_bar, i );

				var modList = new List< int >();
				Int32 mod = 1;
				int j;
				for ( j = 1; j < mPrimeNumber; j++ ) {
					//	mod = (int)Math.Pow( i ,j ) % mPrimeNumber;

					mod *= i;
					mod %= mPrimeNumber;

					Console.Write( format_str_space, mod );
					if ( mod == 1 ) {
						break;
					}
					//	一度出てきた数値をチェックする		//	1以外で重複はしない
					if ( modList.Contains( mod ) ){
						//	重複したらそこで終わり
						Console.Write( "x" );
						break;
					}
					modList.Add( mod );
				}
				if ( j==mPrimeNumber-1 ) {
					// 原始根
					primitiveRoots.Add( i );
				}
				Console.WriteLine( "" );
			}
			
			//	原始根があれば表示
			if ( primitiveRoots.Count>0 ) {
				Console.WriteLine( String.Join( ",", primitiveRoots.Select( x => String.Format( "{0}", x ) ).ToArray() ) );
			}
		}
	}

}
