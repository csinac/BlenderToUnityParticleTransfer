import bpy

class ParticleExporter():
    def __init__(self):
        self.reset()

    def get_particle_system(self, sceneObject):
        if sceneObject == None:
            self.reset()
            return False

        degp = bpy.context.evaluated_depsgraph_get()
        particle_systems = sceneObject.evaluated_get(degp).particle_systems

        if particle_systems == None or len(particle_systems) == 0:
            self.reset()
            return False

        self.__particle_system = particle_systems[0]
        self.__particle_count = len(self.__particle_system.particles)

        return True

    def particle_count(self):
        return self.__particle_count
    
    def is_ready(self):
        return self.__particle_system != None

    def reset(self):
        self.__particle_system = None
        self.__particle_count = 0
