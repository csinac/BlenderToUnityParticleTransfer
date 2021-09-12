import bpy
import sys
import os

dir = os.path.dirname(bpy.data.filepath)
if not dir in sys.path:
    sys.path.append(dir)

from RT_ParticleExporter.ParticleExporter import ParticleExporter

class PEProperties(bpy.types.PropertyGroup):
    ps_container: bpy.props.PointerProperty(name="Particle System", type=bpy.types.Object)
    output_path: bpy.props.StringProperty(name="Output Folder", default="", maxlen=1024, subtype='DIR_PATH')
    filename: bpy.props.StringProperty(name="Filename", default="particles", maxlen=256)

    frame_start: bpy.props.IntProperty(name="Start Frame", default = 1)
    frame_end: bpy.props.IntProperty(name="End Frame", default = 250)
    
    flip_yz: bpy.props.BoolProperty(name="Flip YZ", default=True)
    export_positions: bpy.props.BoolProperty(name="Export Positions", default=True)
    export_velocities: bpy.props.BoolProperty(name="Export Velocities")

    exporter = ParticleExporter()


class PEXPORT_PT_main_panel(bpy.types.Panel):
    bl_label = "Particle Export"
    bl_idname = "PEXPORT_PT_main_panel"
    bl_space_type = 'VIEW_3D'
    bl_region_type = 'UI'
    bl_category = "Particle Export"

    def draw(self, context):
        layout = self.layout
        scene = context.scene
        pe_properties = scene.particle_export

        psRow = layout.row()
        psRow.enabled = False;
        psRow.prop(pe_properties, "ps_container")

        layout.label(text="Particle Count: {}".format(pe_properties.exporter.particle_count()))

        layout.prop(pe_properties, "output_path")
        layout.prop(pe_properties, "filename")

        data_box = layout.box()
        data_box.prop(pe_properties, "flip_yz")
        posRow = data_box.row();
        posRow.enabled = False;
        posRow.prop(pe_properties, "export_positions")
        
        data_box.prop(pe_properties, "frame_start")
        data_box.prop(pe_properties, "frame_end")

        layout.operator("particle_export.get_particles")
        
        exportRow = layout.row()
        exportRow.operator("particle_export.export")
        exportRow.enabled = pe_properties.exporter.is_ready()


class PEXPORT_OT_Export(bpy.types.Operator):
    bl_label = "Export"
    bl_idname = "particle_export.export"

    def execute(self, context):
        scene = context.scene
        pe_properties = scene.particle_export
        
        path = pe_properties.output_path
        filename = pe_properties.filename
        start = pe_properties.frame_start
        end = pe_properties.frame_end
        flip_yz = pe_properties.flip_yz
        
        pe_properties.exporter.export(path, filename, start, end, flip_yz)

        return {'FINISHED'}


class PEXPORT_OT_GetParticles(bpy.types.Operator):
    bl_label = "Get Particles"
    bl_idname = "particle_export.get_particles"

    def execute(self, context):
        scene = context.scene
        pe_properties = scene.particle_export

        selection = bpy.context.selected_objects
        if len(selection) == 0:
            print("You must select an object")
            return {'CANCELLED'}
        elif len(selection) > 1:
            print("You must select a single object")
            return {'CANCELLED'}
        
        if pe_properties.exporter.get_particle_system(selection[0]):
            pe_properties.ps_container = selection[0]
        else:
            pe_properties.ps_container = None

        return {'FINISHED'}


classes = [
    PEProperties,
    PEXPORT_PT_main_panel,
    PEXPORT_OT_Export,
    PEXPORT_OT_GetParticles,
]


def register():
    for cls in classes:
        bpy.utils.register_class(cls)

    bpy.types.Scene.particle_export = bpy.props.PointerProperty(type=PEProperties)


def unregister():
    for cls in classes:
        bpy.utils.unregister_class(cls)

    del bpy.types.Scene.particle_export


if __name__ == "__main__":
    register()