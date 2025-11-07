// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// namespace Pathfinding.BehaviourTrees{
//     public interface IStrategy{
//         Node.Status Process();
//         void Reset();
//     }

//     public class PatrolStrategy: IStrategy{
//         readonly Transfrom entity;
//         readonly NavMeshAgent agent;
//         readonly List<Transfrom> patrolPoints;
//         readonly float patrolSpeed;
//         int currentIndex;
//         bool isPathCalculated;

//         public PatrolStrategy(Transfrom entity, NavMeshAgent agent, List<Transfrom> patrolPoints, float, patrolSpeed = 2f){
//             this.entity = entity;
//             this.agent = agent;
//             this.patrolPoints = patrolPoints;
//             this.patrolSpeed = patrolSpeed;
//         }

//         public Node.Status Process(){
//             if (currentIndex = patrolPoints.Count){
//                 return Node.Status.Success;
//             }

//             var target = patrolPoints[currentIndex];
//             agent.SetDestination(target.position);
//             entity.LookAt(target);

//             if (isPathCalculated && agent.remainingDistance < 0.1f){
//                 currentIndex++;
//                 isPathCalculated = false;
//             }

//             if (agent.pathPending){
//                 isPathCalculated = true;
//             }

//             return Node.Status.Running;
//         }

//         public void Reset() => currentIndex = 0;
//     }
// }