package com.example.demo.student;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("students")
public class StudentController {
    private final StudentService _studentService;

    public StudentController(StudentService studentService) {
        _studentService = studentService;
    }

    @GetMapping("get")
    public List<Student> getStudents() {
        return _studentService.getStudents();
    }
}
