meta:
  id: lhx_elements
  file-extension: lhx_elements
  endian: le
seq:
#  - id: unciphered
#    type: u1
#    repeat: expr
#    repeat-expr: 7

  - id: low_model_chunk
    type: low_model_connection
  
  - id: chunks
    type: chunk
    repeat: eos

types:


  chunk:
    seq:
      - id: lead_byte
        type: u1
      - id: data
        type:
          switch-on: lead_byte
          cases:
            3   : group
            7   : strange_garb_2
            0   : alias
            16  : alias
            24  : alias
            _   : med_model_connection


  low_model_connection:
    seq:
      - id: num_of_elements
        type: u1
      - id: num_of_points
        type: u1
      - id: points_address_prefix
        doc: usually 00 00 00 00
        #contents: [0x00, 0x00, 0x00, 0x00]
        type: u4
      - id: pointcloud_address
        doc:  if zeroes - points are in the separate file
        type: address
      - id: strange1
        type: u2
  
      - id: num_of_polygons
        type: u1
  
      - id: strange2
        type: u1
        
      - id: elements
        type: element
        repeat: expr
        repeat-expr: num_of_elements   

  med_model_connection:
    seq:
      - id: num_of_points
        type: u1
      - id: points_address_prefix
        doc: usually 00 00 00 00
        #contents: [0x00, 0x00, 0x00, 0x00]
        type: u4
      - id: pointcloud_address
        doc:  if zeroes - points are in the separate file
        type: address
      - id: strange1
        type: u2
  
      - id: num_of_polygons
        type: u1
  
      - id: strange2
        type: u1
        
      - id: elements
        type: element
        repeat: expr
        repeat-expr: _parent.lead_byte   
        
  
  address:
    seq:
      - id: segment
        type: u2
      - id: offset
        type: u2

  element:
    seq:
      - id: element_type
        type: u1
        enum: type_of_element
      - id: some_plane_element_id_1
        type: u2
      - id: element_color_code
        type: u1
      - id: opaqueness
        doc: usually 255
        type: u1
      - id: geometry
        type:
          switch-on: element_type
          cases: 
            'type_of_element::separate_line'  : line
            'type_of_element::on_polygon_line': line
            'type_of_element::double_sided_polygon': polygon
            'type_of_element::hidden_polygon': polygon
            'type_of_element::single_sided_polygon': polygon
            'type_of_element::reversed_normals_polygon': polygon
            'type_of_element::point': point
            'type_of_element::sphere': sphere   
        
  polygon:
    seq:
      - id: strange_3
        type: u1
      - id: num_of_points
        type: u1
      - id: points
        type: u1
        repeat: expr
        repeat-expr: num_of_points
        
  line:
    seq:
      - id: points
        type: u1
        repeat: expr
        repeat-expr: 2
        
  point:
    seq:
      - id: point_num_of_point
        type: u1
  
  sphere:
    seq:
      - id: radius
        type: u2
      - id: point_of_center
        type: u1

  group:
    seq:
      - id: num_of_grouped_ids
        type: u1
      - id: grouped_id
        type: u2
        repeat: expr
        repeat-expr: num_of_grouped_ids
  

  alias:
    seq:
      - id: real_id
        type: u2
      - id: alias_id_1
        type: u2
      - id: alias_id_2
        type: u2

  strange_garb_1:
    seq:
      - id: two_zeroes
        type: u2
      - id: ids
        type: u1
        repeat: expr
        repeat-expr: 5
        
  strange_garb_2:
    seq:
      - id: num_of_ids
        type: u1
      - id: ids
        type: u2
        repeat: expr
        repeat-expr: num_of_ids
      - id: zeroes
        type: u1
        repeat: expr
        repeat-expr: 6
      - id: strange_numbers
        type: u1
        repeat: expr
        repeat-expr: 3
        
        

enums:
  type_of_element:
    0x00: double_sided_polygon
    0x01: separate_line
    0x02: point
    0x03: sphere
    0x08: hidden_polygon
    0x10: single_sided_polygon
    0x11: on_polygon_line
    0x18: reversed_normals_polygon


 