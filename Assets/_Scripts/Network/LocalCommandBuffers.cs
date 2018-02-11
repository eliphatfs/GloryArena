using System.Collections;
using System.Collections.Generic;
using System;

public static class LocalCommandBuffers {
	private static Dictionary<int, Queue<object[]>> mTypeMessageDict = new Dictionary<int, Queue<object[]>> ();
	public static void AddMessage (int type, object[] data) {
		if (mTypeMessageDict.ContainsKey (type))
			mTypeMessageDict [type].Enqueue (data);
		else {
			mTypeMessageDict.Add (type, new Queue<object[]> ());
			mTypeMessageDict [type].Enqueue (data);
		}
	}
	public static object[] PeekMessageOfType (int type) {
		if (!mTypeMessageDict.ContainsKey (type))
			return null;
		if (mTypeMessageDict [type].Count == 0)
			return null;
		return mTypeMessageDict [type].Peek ();
	}

	public static object[] PollMessageOfType (int type) {
		if (!mTypeMessageDict.ContainsKey (type))
			return null;
		if (mTypeMessageDict [type].Count == 0)
			return null;
		return mTypeMessageDict [type].Dequeue ();
	}

	public static bool HasMoreMessages (int type) {
		return mTypeMessageDict.ContainsKey (type) && mTypeMessageDict [type].Count > 0;
	}
}
