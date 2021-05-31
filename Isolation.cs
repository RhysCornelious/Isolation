using System;
using static System.Console;
using static System.Math;

// this is a test

namespace Bme121
{
    static class Program
    {
        static int width; // width of board
        static int height; // height of board
        static int p1StartX; // where player a starts on x axis
        static int p1StartY; // where player a starts on y axis
        static int p2StartX; // where player b starts on x axis
        static int p2StartY; // where player b starts on y axis
        static int platform1X; // position of player a's starting platform in x axis
        static int platform1Y; // position of player a's starting platform in y axis
        static int platform2X; // position of player b's starting platform in x axis
        static int platform2Y; // position of player b's starting platform in y axis
        static int pAX; // player a's location on x axis
        static int pAY; // player a's location on y axis
        static int pBX; // player b's location on x axis
        static int pBY; // player b's location on y axis
        static string name1;
        static string name2;
        static string moveA;
        static string moveB;
        static bool gameRun;
        
        static bool[ , ] board;
        
        static void Main( )
        {
            gameRun = true;
            Initialization( );
            DrawGameBoard( );
            while( gameRun == true )
            {
               
                MovesA( );
                Console.Clear( );
                DrawGameBoard( );
                MovesB( );
                Console.Clear( );
                DrawGameBoard( );
            }
        }
        
        static void Initialization( )
        {
            string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"}; // letters to numbers string
            // Collect user input but allow just <Enter> for a default value.
            Write( "Enter your name [default Player A]: " );
            name1 = ReadLine( );
            if( name1.Length == 0 ) name1 = "Player A";
            WriteLine( "Name: {0}", name1 );
            Write( "Enter your name [default Player B]: " );
            name2 = ReadLine( );
            if( name2.Length == 0 ) name2 = "Player B";
            WriteLine( "Name: {0}", name2 );
            
            // Collecting input for size of board, defaults to 6x8 if outside array
            //ask about how to get defaults just by pressing enter
            Write( "Please enter desired board width, between 4 (d) and 26 (z): " );
            string widthS = ReadLine( );
            if( widthS.Length == 0 )
            {
                width = 6;
                height = 8;
            }
            else
            {
                width = Array.IndexOf( letters, widthS );
                width = width + 1;
                if ( width < 4 || width > 26 )
                {
                    WriteLine( "Width was outside of mandatory bounds. Setting board size to default of 6 x 8." );
                    width = 6;
                    height = 8;
                }
                else
                {
                    Write( "Please enter desired board height, between 4 (d) and 26 (z): " );
                    string heightS = ReadLine( );
                    if( heightS.Length == 0 )
                    {
                        width = 6;
                        height = 8;
                    }
                    else
                    {
                        height = Array.IndexOf( letters, heightS );
                        height = height + 1;
                        if ( height < 4 || height > 26 )
                        {
                            WriteLine( "Height was outside of mandatory bounds. Setting board size to default of 6 x 8." );
                            height = 8;
                        }
                    }
                }
            }
            WriteLine( "Board dimensions: {0}x{1}", width, height );
            
            //Getting input for starting positions for both players, defaults listed
            //ask about how to get defaults by just pressing enter
            Write( "{0}, Please enter desired starting location on x axis [default c]: ", name1 );
            string p1StartXS = ReadLine( );
            p1StartX = Array.IndexOf( letters, p1StartXS );
            if ( p1StartX < 0 || p1StartX > width )
            {
                WriteLine( "Desired starting position was off of the board, starting position set to default." );
                p1StartX = 2;
                p1StartY = 0;
            }
            else
            {
                Write( "{0}, please enter desired starting location on y axis [default a]: ", name1 );
                string p1StartYS = ReadLine( );
                p1StartY = Array.IndexOf( letters, p1StartYS );
                if ( p1StartY < 0 || p1StartY > height )
                {
                    WriteLine( "Desired starting position was off of the board, starting position set to default." );
                    p1StartX = 2;
                    p1StartY = 0;
                }
            }
            
            Write( "{0}, please enter desired starting location on x axis [default d]: ", name2 );
            string p2StartXS = ReadLine( );
            p2StartX = Array.IndexOf( letters, p2StartXS );
            if ( p2StartX < 0 || p2StartX > width )
            {
                WriteLine( "Desired starting position was off of the board, starting position set to default." );
                p2StartX = 3;
                p2StartY = 7;
            }
            else
            {
                Write( "{0}, Please enter desired starting location on y axis [default h]: ", name2 );
                string p2StartYS = ReadLine( );
                p2StartY = Array.IndexOf( letters, p2StartYS );
                if ( p2StartY < 0 || p2StartY > height )
                {
                    WriteLine( "Desired starting position was off of the board, starting position set to default." );
                    p2StartX = 3;
                    p2StartY = 7;
                }
            }
            
            platform1X = p1StartX;
            platform1Y = p1StartY;
            platform2X = p2StartX;
            platform2Y = p2StartY;
            pAX = p1StartX;
            pAY = p1StartY;
            pBX = p2StartX;
            pBY = p2StartY;
            board = new bool[ width, height ];
            
            WriteLine( "Board dimensions: {0}x{1}", width, height );
            WriteLine( "{0}'s starting position is {1},{2}", name1, p1StartX, p1StartY );
            WriteLine( "{0}'s starting position is {1},{2}", name2, p2StartX, p2StartY );
            WriteLine( "Platform 1's location is {0},{1}", platform1X, platform1Y );
            WriteLine( "Platfrom 2's location position is {0},{1}", platform2X, platform2Y );
        
        }
        
        
        static void DrawGameBoard( )
        {
            const string h  = "\u2500"; // horizontal line
            const string v  = "\u2502"; // vertical line
            const string tl = "\u250c"; // top left corner
            const string tr = "\u2510"; // top right corner
            const string bl = "\u2514"; // bottom left corner
            const string br = "\u2518"; // bottom right corner
            const string vr = "\u251c"; // vertical join from right
            const string vl = "\u2524"; // vertical join from left
            const string hb = "\u252c"; // horizontal join from below
            const string ha = "\u2534"; // horizontal join from above
            const string hv = "\u253c"; // horizontal vertical cross
            const string sp = " ";      // space
            const string pa = "A";      // pawn A
            const string pb = "B";      // pawn B
            const string bb = "\u25a0"; // block
            const string fb = "\u2588"; // middle block
            const string lh = "\u258c"; // left half block
            const string rh = "\u2590"; // right half block
            
            string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"}; // letters to numbers string
                
            Write("   ");
            for( int q = 0; q < board.GetLength( 0 ); q ++ )
            {
                Write( "  {0} ", letters[ q ] );
            }
            WriteLine();
            
            // Draw the top board boundary.
            Write( "   " );
            for( int c = 0; c < board.GetLength( 0 ); c ++ )
            {
                if( c == 0 ) Write( tl );
                Write( "{0}{0}{0}", h );
                if( c == board.GetLength( 0 ) - 1 ) Write( "{0}", tr ); 
                else                                Write( "{0}", hb );
            }
            WriteLine( );
            
            // Draw the board rows.
            for( int r = 0; r < board.GetLength( 1 ); r ++ )
            {
                Write( " {0} ", letters[ r ] );
                
                // Draw the row contents.
                for( int c = 0; c < board.GetLength( 0 ); c ++ )
                {
                    if( c == 0 ) Write( v );
                    if( ( r == pAY ) && ( c == pAX ) ) Write( "{0}{1}{2}{3}", sp, pa, sp, v );
                    else if( ( r == pBY ) && ( c == pBX  ) ) Write( "{0}{1}{2}{3}", sp, pb, sp, v );
                    else if( ( r == platform1Y  ) && ( c == platform1X  ) ) Write( "{0}{1}{2}{3}", sp, bb, sp, v );
                    else if( ( r == platform2Y  ) && ( c == platform2X  ) ) Write( "{0}{1}{2}{3}", sp, bb, sp, v );
                    else if( board[ c, r ] == false ) Write( "{0}{1}{2}{3}", rh, fb, lh, v );
                    else Write( "{0}{1}", "   ", v );
                }
                WriteLine( );
                
                // Draw the boundary after the row.
                if( r != board.GetLength( 1 ) - 1 )
                { 
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 0 ); c ++ )
                    {
                        if( c == 0 ) Write( vr );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 0 ) - 1 ) Write( "{0}", vl ); 
                        else                                Write( "{0}", hv );
                    }
                    WriteLine( );
                }
                else
                {
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 0 ); c ++ )
                    {
                        if( c == 0 ) Write( bl );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 0 ) - 1 ) Write( "{0}", br ); 
                        else                                Write( "{0}", ha );
                    }
                    WriteLine( );
                }
            }
        }
        
        static void MovesA( )
        {
            string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"}; // letters to numbers string
            bool validMove = false; // set true to allow move if follows logic of game
            
            int oldMoveX = pAX; // setting old move so can't stay in one place
            int oldMoveY = pAY; // setting old move so can't stay in one place
            int removeTileX = 0; // setting base value of where tile will be removed
            int removeTileY = 0; // setting base value of where tile will be removed
            
            while( validMove == false )
            {
                Write( "{0}, please enter move: ", name1 );
                moveA = ( ReadLine( ) );
                if( moveA.Length < 4 || moveA.Length > 4 ) Write( "" );
                else
                {
                    pAX = Array.IndexOf( letters, moveA.Substring( 0, 1 ) );
                    pAY = Array.IndexOf( letters, moveA.Substring( 1, 1 ) );
                    removeTileX = Array.IndexOf( letters, moveA.Substring( 2, 1 ) );
                    removeTileY = Array.IndexOf( letters, moveA.Substring( 3, 1 ) );
                    if( ( Abs ( pAX - oldMoveX ) <= 1 ) && ( Abs ( pAY - oldMoveY ) <=1 ) && ( (pAX!=oldMoveX) || (pAY!=oldMoveY)) && ( (pAX!=pBX) || (pAY!=pBY) ) 
                    && ( pAX <= board.GetLength( 0 ) - 1 ) && ( pAY <= board.GetLength( 1 ) - 1 ) && ( board[ pAX, pAY ] == false ) 
                    && ( removeTileX <= board.GetLength( 0 ) - 1 ) && ( removeTileY <= board.GetLength( 1 ) - 1 )   
                    && ( (pAX!=removeTileX) || (pAY!=removeTileY) ) && ( (pBX!=removeTileX) || (pBY!=removeTileY) ) 
                    && ( (platform1X!=removeTileX) || (platform1Y!=removeTileY) ) && ( (platform2X!=removeTileX) || (platform2Y!=removeTileY) ) 
                    && ( board[ removeTileX, removeTileY ] == false ) )
                    {
                        validMove = true;
                        for( int r = 0; r < board.GetLength( 1 ); r ++ )
                        {
                            for( int c = 0; c < board.GetLength( 0 ); c ++ )
                            {
                                if( ( r == removeTileY ) && ( c == removeTileX  ) )
                                {
                                    board[ c, r ] = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        WriteLine( "Move not valid." );
                    }
                }
            }
        }
        
        static void MovesB( )
        {
            string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"}; // letters to numbers string
            bool validMove = false; // set true to allow move if follows logic of game
            
            int oldMoveX = pBX; // setting old move so can't stay in one place
            int oldMoveY = pBY; // setting old move so can't stay in one place
            int removeTileX = 0; // setting base value of where tile will be removed
            int removeTileY = 0; // setting base value of where tile will be removed
            
            while( validMove == false )
            {
                Write( "{0}, please enter move: ", name2 );
                moveB = ( ReadLine( ) );
                if( moveB.Length < 4 || moveB.Length > 4 ) Write( "" );
                else
                {
                    pBX = Array.IndexOf( letters, moveB.Substring( 0, 1 ) );
                    pBY = Array.IndexOf( letters, moveB.Substring( 1, 1 ) );
                    removeTileX = Array.IndexOf( letters, moveB.Substring( 2, 1 ) );
                    removeTileY = Array.IndexOf( letters, moveB.Substring( 3, 1 ) );
                    if( ( Abs ( pBX - oldMoveX ) <= 1 ) && ( Abs ( pBY - oldMoveY ) <=1 ) && ( (pBX!=oldMoveX) || (pBY!=oldMoveY)) && ( (pBX!=pAX) || (pBY!=pAY) )
                    && ( removeTileX <= board.GetLength( 0 ) - 1 ) && ( removeTileY <= board.GetLength( 1 ) - 1 )
                    && ( pBX <= board.GetLength( 0 ) - 1 ) && ( pBY <= board.GetLength( 1 ) - 1 ) && ( board[ pBX, pBY ] == false ) 
                    && ( (pBX!=removeTileX) || (pBY!=removeTileY) ) && ( (pAX!=removeTileX) || (pAY!=removeTileY) ) 
                    && ( (platform2X!=removeTileX) || (platform2Y!=removeTileY) ) && ( (platform1X!=removeTileX) || (platform1Y!=removeTileY) ) 
                    && ( board[ removeTileX, removeTileY ] == false ) )
                    {
                        validMove = true;
                        for( int r = 0; r < board.GetLength( 1 ); r ++ )
                        {
                            for( int c = 0; c < board.GetLength( 0 ); c ++ )
                            {
                                if( ( r == removeTileY ) && ( c == removeTileX  ) )
                                {
                                    board[ c, r ] = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        WriteLine( "Move not valid." );
                    }
                }
            }
        }
    }
}

