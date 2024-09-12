meta:
  id: lhx_points
  file-extension: pnt
  endian: le
seq:
  - id: header
    type: header_info
    
    #s1
  - id: dotcloud_low
    type: dotcloud
    size: header.set1_end_offset - header.set1_start_offset
 
    # bsp 1_1
  - id: dotcloud_low_bsp_1
    doc: bsp 1 till the end of file -> s110s200
    if: header.set1_bsp_end_offset == 0 and header.set2_start_offset == 0
    type: u1
    repeat: eos
    
    # bsp 1_2
  - id: dotcloud_low_bsp_2
    doc: bsp 1 between set 1 and addon 1 -> s111s200 s111s210 s111s210
    if: header.set1_bsp_end_offset != 0
    type: u1
    repeat: expr
    repeat-expr: header.set1_bsp_end_offset - header.set1_end_offset
    
    # bsp 1_3
  - id: dotcloud_low_bsp_3
    doc: bsp 1 between s1 and s2 -> s110s210 s110s211
    if: header.set1_bsp_end_offset == 0 and header.set2_start_offset != 0
    type: u1
    repeat: expr
    repeat-expr: header.set2_start_offset - header.set1_end_offset
    
    # a 1_1 == not exist -> s110s200 s110s210 s110s111
    
    # a 1_2
  - id: dotcloud_low_addon_2
    doc: addon 1 till end of file -> s111s200
    if: header.set1_bsp_end_offset != 0 and header.set2_start_offset == 0
    type: u1
    repeat: eos

    # a 1_3
  - id: dotcloud_low_addon_3
    doc: addon between bsp 1 and set 2 -> s111s210 s111s211
    if: header.set1_bsp_end_offset != 0 and header.set2_start_offset != 0
    type: u1
    repeat: expr
    repeat-expr: header.set2_start_offset - header.set1_bsp_end_offset
    
    #s2
  - id: dotcloud_med
    type: dotcloud
    if: header.set2_start_offset != 0
    size: header.set2_end_offset - header.set2_start_offset
    
    # bsp 2_1 == not exist -> s110s200 s111s200
    
    # bsp 2_2
  - id: dotcloud_med_bsp_2
    doc: bsp 2 till end of file -> s110s210 s111s210
    if: header.set2_start_offset != 0 and header.set2_bsp_end_offset == 0
    type: u1
    repeat: eos
    
    # bsp 2_3
  - id: dotcloud_med_bsp_3
    doc: bsp 2 between set 2 and addon 2
    if: header.set2_start_offset != 0 and header.set2_bsp_end_offset != 0
    type: u1
    repeat: expr
    repeat-expr: header.set2_bsp_end_offset - header.set2_end_offset 
    
    # a 2_1 == not exist -> s110s200 s111s200 s110s210 s111s210
  - id: dotcloud_med_addon_2
    doc: addon 2 till end of file -> s110s211 s111s211
    if: header.set2_start_offset != 0 and header.set2_bsp_end_offset != 0
    type: u1
    repeat: eos



    
types:
  header_info:
    seq:
        - id: zerostart
          type: u2
        - id: set1_start_offset
          type: u2
          doc: Number of bytes to skip from the beginning of file.   
        - id: set2_start_offset
          type: u2
          doc: Number of bytes to skip from the beginning of file. Equals zero if there is no set 2 
        - id: set1_end_offset
          type: u2
          doc: Number of bytes to skip from the beginning of file. 
        - id: set2_end_offset
          type: u2
          doc: Number of bytes to skip from the beginning of file. Equals zero if there is no set 2
        - id: set1_bsp_end_offset
          type: u2
        - id: set2_bsp_end_offset
          type: u2  
        - id: strange3
          type: u2  
        - id: strange4
          type: u2  
  dotcloud:
    seq:
        - id: dots
          type: dot
          repeat: eos
  dot:
    seq:
    - id: x
      type: s2
    - id: z
      type: s2
    - id: y
      type: s2
  
