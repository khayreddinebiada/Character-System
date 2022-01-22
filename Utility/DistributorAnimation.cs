using System.Collections.Generic;
using UnityEngine;

namespace characters
{
    public partial class DistributorAnimation
    {
        // Consts.
        private const int _defaultCapacity = 30;

        // Structs
        private struct Destinations
        {
            public Transform[] destinations;

            internal Destinations(Transform[] destinations)
            {
                this.destinations = destinations;
            }

            public void Past(Transform[] sources)
            {
                if (sources == null) throw new System.NullReferenceException();

                for (int i = 0; i < sources.Length && i < destinations.Length; i++)
                {
                    if (destinations[i] == null)
                        continue;

                    destinations[i].localPosition = sources[i].localPosition;
                    destinations[i].localRotation = sources[i].localRotation;
                    destinations[i].localScale = sources[i].localScale;
                }
            }
        }

        // Privates
        private List<Destinations> roots = new List<Destinations>();
        private Transform[] sources;
        private int currentFrame = 0;

        // Publics
        public int framesToExecute { get; set; }

        /// <summary>
        /// Create the Distributor for copy the animations from source to all destinations.
        /// </summary>
        /// <param name="skinnedMeshRenderer"> Is the source root bone where we copy the animations. </param>
        /// <param name="framesToExecute"> How much frames to copy all animations. </param>
        public DistributorAnimation(SkinnedMeshRenderer skinnedMeshRenderer, int framesToExecute = 1)
        {
            if (skinnedMeshRenderer == null) throw new System.ArgumentNullException();

            List<Transform> sources = new List<Transform>(_defaultCapacity);

            GetChildrenTransforms(skinnedMeshRenderer.rootBone, sources);
            this.sources = sources.ToArray();

            currentFrame = this.framesToExecute = framesToExecute;
        }

        /// <summary>
        /// Create the Distributor for copy the animations from source to all destinations.
        /// </summary>
        /// <param name="sourceRootAnimation"> Is the source transform where we copy the animations. </param>
        /// <param name="framesToExecute"> How much frames to copy all animations. </param>
        public DistributorAnimation(Transform sourceRootAnimation, int framesToExecute = 1)
        {
            if (sourceRootAnimation == null) throw new System.ArgumentNullException();

            List<Transform> sources = new List<Transform>(_defaultCapacity);

            GetChildrenTransforms(sourceRootAnimation, sources);
            this.sources = sources.ToArray();

            currentFrame = this.framesToExecute = framesToExecute;
        }

        /// <summary>
        /// When you call this function you will past all the animations.
        /// </summary>
        public void Animate()
        {
            int executePerFrame = roots.Count / framesToExecute;
            int from = executePerFrame * (currentFrame - 1);
            int to = executePerFrame * (currentFrame);

            /// Execute the list of animates.
            for (int i = from; i < roots.Count && i <= to; i++)
            {
                roots[i].Past(sources);
            }

            /// Update the frame number.
            if (currentFrame <= 1)
                currentFrame = framesToExecute;
            else
                currentFrame--;
        }

        /// <summary>
        /// We will add the root bone with children transform to the list. for copy the animation from source to all roots.
        /// </summary>
        /// <param name="skinned"> The SkinnedMeshRenderer of the animation that we will animate it. </param>
        public void AddRoot(SkinnedMeshRenderer skinned)
        {
            AddRoot(skinned.rootBone);
        }

        /// <summary>
        /// We will add the root with children transform to the list for copy the animation from source to all roots.
        /// </summary>
        /// <param name="root"> The parent of the animation</param>
        public void AddRoot(Transform root)
        {
            if (root == null) throw new System.NullReferenceException();
            List<Transform> transforms = new List<Transform>(_defaultCapacity);

            GetChildrenTransforms(root, transforms);

            Destinations node = new Destinations(transforms.ToArray());
            roots.Add(node);
            node.Past(sources);
        }

        /// <summary>
        /// Enter root paramter to get the all transforms inside the root.
        /// </summary>
        /// <param name="result"> The result of all transform </param>
        private void GetChildrenTransforms(Transform root, List<Transform> result)
        {
            if (root != null)
            {
                result.Add(root);
                for (int i = 0; i < root.childCount; i++)
                {
                    GetChildrenTransforms(root.GetChild(i), result);
                }
            }
        }
    }
}