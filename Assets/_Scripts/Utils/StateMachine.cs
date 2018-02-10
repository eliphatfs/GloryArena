using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour {
	protected Dictionary<string, List<Func<string>>> Transitions = new Dictionary<string, List<Func<string>>> ();
	private Dictionary<string, Action> EnterEvent = new Dictionary<string, Action> (),
	ExitEvent = new Dictionary<string, Action> ();
	private string mCurrentState;
	public string CurrentState {
		get {
			return mCurrentState;
		}
		private set {
			if (ExitEvent.ContainsKey (mCurrentState))
				ExitEvent [mCurrentState].Invoke ();
			mCurrentState = value;
			if (EnterEvent.ContainsKey (value))
				EnterEvent [value].Invoke ();
		}
	}

	void Start () {
		InitializeStates ();
		InitializeTransitions ();
		if (!Transitions.ContainsKey (InitialState))
			throw new InvalidOperationException (string.Format ("Initial state {0} is not registered!", InitialState));
		CurrentState = InitialState;
	}

	void Update () {
		foreach (Func<string> transition in Transitions[CurrentState]) {
			string ret_val = transition.Invoke ();
			if (ret_val != null) {
				if (!Transitions.ContainsKey (ret_val))
					throw new InvalidOperationException (string.Format ("State {0} is not registered!", ret_val));
				CurrentState = ret_val;
				break;
			}
		}
	}

	/// <summary>
	/// 加入一个新的状态。如果该状态已存在会报InvalidOpreationException.
	/// </summary>
	/// <param name="stateName">新状态的名字</param>
	protected void RegisterState (string stateName, Action enterAction = null, Action exitAction = null) {
		if (Transitions.ContainsKey (stateName))
			throw new InvalidOperationException (string.Format ("State {0} already registered!", stateName));
		Transitions.Add (stateName, new List<Func<string>> ());
		if (enterAction != null)
			EnterEvent.Add (stateName, enterAction);
		if (exitAction != null)
			ExitEvent.Add (stateName, exitAction);
	}

	/// <summary>
	/// 加入一个状态转移。
	/// </summary>
	/// <param name="fromState">当前状态</param>
	/// <param name="transition">一个函数，如果返回null则状态不会改变，如果返回非null则转换到另一状态。</param>
	protected void RegisterTransition (string fromState, Func<string> transition) {
		if (!Transitions.ContainsKey (fromState))
			throw new ArgumentOutOfRangeException (string.Format ("State {0} is not registered!", fromState));
		Transitions [fromState].Add (transition);
	}

	protected void ForceSetState (string newState) {
		if (!Transitions.ContainsKey (newState))
			throw new ArgumentOutOfRangeException (string.Format ("State {0} is not registered!", newState));
		CurrentState = newState;
	}
	
	protected abstract string InitialState { get; }
	protected abstract void InitializeStates ();
	protected abstract void InitializeTransitions ();
}
