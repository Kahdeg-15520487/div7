using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace divisibleby7
{
    public class DFA<TState, TAction>
    {
        private readonly List<TState> Q = new List<TState>();
        private readonly List<TAction> Sigma = new List<TAction>();
        private readonly List<Transition<TState, TAction>> Delta = new List<Transition<TState, TAction>>();
        private TState Q0;
        private readonly List<TState> F = new List<TState>();

        public DFA(IEnumerable<TState> q, IEnumerable<TAction> sigma, IEnumerable<Transition<TState, TAction>> delta, TState q0, IEnumerable<TState> f)
        {
            Q = q.ToList();
            Sigma = sigma.ToList();
            AddTransitions(delta);
            AddInitialState(q0);
            AddFinalStates(f);

        }

        public bool Accepts(IEnumerable<TState> input, out List<Transition<TState, TAction>> steps)
        {
            var currentState = Q0;
            steps = new List<Transition<TState, TAction>>();
            foreach (var a in input)
            {
                var transition = Delta.Find(d => d.Start.Equals(currentState) && d.Action.Equals(a));
                if (transition == null)
                {
                    throw new Exception($"No transitions exist for current state({currentState}) and action({a})");
                }
                currentState = transition.End;
                steps.Add(transition);
            }
            if (F.Contains(currentState))
            {
                return true;
            }
            return false;
        }

        private void AddTransitions(IEnumerable<Transition<TState, TAction>> delta)
        {
            foreach (var d in delta.Where(ValidTransition))
            {
                Delta.Add(d);
            }
        }

        private bool ValidTransition(Transition<TState, TAction> d)
        {
            return Q.Contains(d.Start)
            && Q.Contains(d.End)
            && Sigma.Contains(d.Action)
            && !TransitionAlreadyDefined(d);
        }

        private bool TransitionAlreadyDefined(Transition<TState, TAction> d)
        {
            return Delta.Any(t => t.Start.Equals(d.Start) && t.Action.Equals(d.Action));

        }

        private void AddInitialState(TState q0)
        {
            if (Q.Contains(q0))
            {
                Q0 = q0;
            }
        }

        private void AddFinalStates(IEnumerable<TState> finalStates)
        {
            foreach (var f in finalStates.Where(fs => Q.Contains(fs)))
            {
                F.Add(f);
            }
        }
    }

    public class Transition<TState, TAction>
    {
        public Transition(TState start, TAction action, TState end)
        {
            Start = start;
            Action = action;
            End = end;
        }

        public TState Start { get; set; }
        public TAction Action { get; set; }
        public TState End { get; set; }

        public override string ToString()
        {
            return string.Format("({0}, {1}) -> {2}", Start, Action, End);
        }
    }
}
