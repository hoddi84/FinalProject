namespace GPUInstanced {

	public static class GPUInstancedDrawer {

		public static void DrawInstancedIndirect(GPUInstancedNodeGroup instancedNodeGoup)
		{
			UnityEngine.Graphics.DrawMeshInstancedIndirect(
				instancedNodeGoup.mesh, 
				0, 
				instancedNodeGoup.material, 
				instancedNodeGoup.bounds, 
				instancedNodeGoup.bufferArgs
			);
		}
	}
}


