package com.example.demo.student;

import org.springframework.http.client.OkHttp3ClientHttpRequestFactory;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("students")
public class StudentController {
    private final StudentService _studentService;

    public StudentController(StudentService studentService) {
        _studentService = studentService;
    }

    @GetMapping
    public List<Student> getStudents() {
        return _studentService.getStudents();
    }
    
    @PostMapping
    public Student registerStudent(@RequestBody Student student){
        return _studentService.registerStudent(student);
    }
}
